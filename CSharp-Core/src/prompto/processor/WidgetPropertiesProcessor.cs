using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.error;
using prompto.grammar;
using prompto.literal;
using prompto.property;
using prompto.runtime;
using prompto.type;
using prompto.value;

namespace prompto.processor
{
    public class WidgetPropertiesProcessor : AnnotationProcessor
    {
        public override void ProcessCategory(Annotation annotation, Context context, CategoryDeclaration declaration)
        {
            if (declaration.IsAWidget(context))
                DoProcessCategory(annotation, context, declaration);
            else
                throw new SyntaxError("WidgetProperties is only applicable to widgets");
        }

        private void DoProcessCategory(Annotation annotation, Context context, CategoryDeclaration declaration)
        {
            IWidgetDeclaration widget = declaration.AsWidget();
            Object value = annotation.GetDefaultArgument();
            PropertyMap properties = CheckProperties(annotation, context, value);
            if (properties != null)
            {
                widget.SetProperties(properties);
                Annotation widgetField = FindWidgetPropertiesFieldAnnotation(context, declaration);
                if (widgetField != null)
                    OverrideWidgetFieldType(context, widgetField, new PropertiesType(properties));
            }
        }

		private Annotation FindWidgetPropertiesFieldAnnotation(Context context, CategoryDeclaration declaration)
		{
			foreach (Annotation a in declaration.GetAllAnnotations(context))
			{
				if ("@WidgetField" != a.Name)
					continue;
				Object value = a.GetArgument("isProperties");
				if (value is BooleanLiteral && ((BooleanValue)((BooleanLiteral)value).getValue()).Value)
					return a;
			}
			return null;
		}

		private void OverrideWidgetFieldType(Context context, Annotation widgetField, IType type)
		{
			Object value = widgetField.GetArgument("name");
			if (!(value is TextLiteral))	
			    return; // TODO raise warning
			String name = ((TextLiteral)value).ToString();
			InstanceContext instance = context.getClosestInstanceContext();
			if (instance == null)
				throw new InternalError("Expected an instance context. Please report this bug.");
			instance.RegisterWidgetField(name.Substring(1, name.Length - 2), type, true);
		}



		private PropertyMap CheckProperties(Annotation annotation, Context context, Object value)
        {
            if (!(value is DocumentLiteral))
            {
                throw new SyntaxError("WidgetProperties expects a Document of types as unique parameter");
            }
            return LoadProperties(annotation, context, ((DocumentLiteral)value).GetEntries());
        }

        public PropertyMap LoadProperties(Annotation annotation, Context context, DocEntryList entries)
        {
            PropertyMap props = new PropertyMap();
            foreach (DictEntry entry in entries)
            {
                Property prop = LoadProperty(annotation, context, entry);
                if (prop == null)
                    continue;
                if (props.ContainsKey(prop.GetName()))
                    throw new SyntaxError("Duplicate property: " + prop.GetName());
                else
                    props[prop.GetName()] = prop;
            }
            return props;
        }

        private Property LoadProperty(Annotation annotation, Context context, DictEntry entry)
        {
            Property prop = new Property();
            prop.SetName(entry.GetKey().ToString());
            Object value = entry.GetValue();
            if (value is TypeLiteral)
                return LoadProperty(annotation, context, entry, prop, (TypeLiteral)value);
            else if (value is SetLiteral)
                return LoadProperty(annotation, context, entry, prop, (SetLiteral)value);
            else if (value is DocumentLiteral)
                return LoadProperty(annotation, context, entry, prop, (DocumentLiteral)value);
            else
                throw new SyntaxError("WidgetProperties expects a Document of types as unique parameter");
        }

		private Property LoadProperty(Annotation annotation, Context context, DictEntry entry, Property prop, DocumentLiteral doc)
		{
			foreach (DictEntry child in doc.GetEntries())
			{
				String name = child.GetKey().ToString();
				Object value = child.GetValue();
				switch (name)
				{
					case "required":
						if (value is BooleanLiteral) {
				            prop.SetRequired(((BooleanLiteral)value).interpret(context) == BooleanValue.TRUE);
				            break;
			            }
						throw new SyntaxError("Expected a Boolean value for 'required'.");
			    case "help":
				    if (value is TextLiteral) {
				        prop.SetHelp((String)((TextLiteral)value).getValue().GetStorableData());
				        break;
			        }
					throw new SyntaxError("Expected a Text value for 'help'.");
			    case "type":
				    if (value is TypeLiteral) {
				        IType type = resolveType(annotation, context, (TypeLiteral)value);
				        if (type == null)
					        return null;
				        prop.SetValidator(new TypeValidator(type));
				        break;
			        } else if (value is DocumentLiteral) {
				        PropertyMap embedded = LoadProperties(annotation, context, ((DocumentLiteral)value).GetEntries());
				        if (embedded != null)
				        {
					        prop.SetValidator(new TypeValidator(new PropertiesType(embedded)));
					        break;
				        }
			        }
					throw new SyntaxError("Expected a Type value for 'type'.");
			    case "types":
				    if (value is SetLiteral) {
				        SetValue values = (SetValue)((SetLiteral)value).interpret(context);
				        if (values.ItemType is TypeType) {
							HashSet<IType> types = new HashSet<IType>();
                            foreach(IValue val in values.getItems())
                            {
								if (val == NullValue.Instance)
									continue;
								types.Add(resolveType(annotation, context, ((TypeValue)val).GetIType()));
                            }
					        if (types.Contains(null))
						        return null; // TODO something went wrong
					        prop.SetValidator(new TypeSetValidator(types));
					        prop.SetRequired(types.Count == values.Length()); // no null filtered out
					        break;
				        }
			        }
					throw new SyntaxError("Expected a Set of types for 'types'.");
			    case "values":
				    if (value is SetLiteral) {
				        SetValue values = (SetValue)((SetLiteral)value).interpret(context);
							HashSet<String> texts = new HashSet<String>();
							foreach (IValue val in values.getItems())
                            {
								if (val == NullValue.Instance)
									continue;
								texts.Add(val.GetStorableData().ToString());
							}
						prop.SetValidator(new ValueSetValidator(texts));
				        prop.SetRequired(texts.Count == values.Length()); // no null filtered out
				        break;
			        }
					throw new SyntaxError("Expected a Set value for 'values'.");
			    default:
					throw new SyntaxError("Unknown property attribute: " + name);
		    }
	    }
		return prop;
	}

    private Property LoadProperty(Annotation annotation, Context context, DictEntry entry, Property prop, SetLiteral literal)
    {
	    SetValue values = (SetValue)literal.interpret(context);
	    IType itemType = values.ItemType;
	    if (itemType is TypeType) {
				HashSet<IType> types = new HashSet<IType>();
                foreach(IValue val in values.getItems()) {
					if (val == NullValue.Instance)
						continue;
					types.Add(resolveType(annotation, context, ((TypeValue)val).GetValue()));
				}
		        if (types.Contains(null))
			        return null; // something went wrong
		        prop.SetValidator(new TypeSetValidator(types));
		        prop.SetRequired(types.Count == values.Length()); // no null filtered out
		     return prop;
	    }
        else if (itemType == AnyType.Instance || itemType == TextType.Instance)
	    {
				HashSet<String> texts = new HashSet<String>();
				foreach (IValue val in values.getItems()) {
					if (val == NullValue.Instance)
						continue;
					texts.Add(val.ToString());
				}
		    prop.SetValidator(new ValueSetValidator(texts));
		    prop.SetRequired(texts.Count == values.Length()); // no null filtered out
		    return prop;
	    }
	    else
			throw new SyntaxError("Expected a set of Types.");
	}

    private Property LoadProperty(Annotation annotation, Context context, DictEntry entry, Property prop, TypeLiteral value)
    {
	    IType type = resolveType(annotation, context, value);
	    if (type == null)
		    return null;
	    prop.SetValidator(new TypeValidator(type));
	    return prop;
    }

    private IType resolveType(Annotation annotation, Context context, TypeLiteral value)
    {
	    return resolveType(annotation, context, value.getType());
    }

    private IType resolveType(Annotation annotation, Context context, IType type)
    {
	    type = type.Anyfy();
	    if (type is NativeType)
			return type;
		else
	    {
		    IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(type.GetTypeName());
			    if(decl==null)
					throw new SyntaxError("Unkown type: " + type.GetTypeName());
			    else if(decl is MethodDeclarationMap)
				    return new MethodType(((MethodDeclarationMap) decl).GetFirst());
			    else
				    return decl.GetIType(context);
		    }
	    }
    }
}
