using System;
using System.Collections.Generic;
using prompto.expression;
using prompto.runtime;
using prompto.store;
using prompto.value;

namespace prompto.type
{

	public abstract class NativeType : BaseType
	{

		private static NativeType[] all = null;

		public static NativeType[] getAll()
		{
			if (all == null)
			{
				all = new NativeType[] {
					AnyType.Instance,
					BooleanType.Instance,
					IntegerType.Instance,
					DecimalType.Instance,
					CharacterType.Instance,
					TextType.Instance,
					CodeType.Instance,
					DateType.Instance,
					TimeType.Instance,
					DateTimeType.Instance,
					PeriodType.Instance,
					DocumentType.Instance,
					TupleType.Instance
				};
			}
			return all;
		}

		public NativeType(TypeFamily family)
			: base(family)
		{
		}


		public override IType checkMember(Context context, string name)
		{
			if ("text" == name)
				return TextType.Instance;
			else
				return base.checkMember(context, name);
		}

		public override void checkUnique(Context context)
		{
			// nothing to do
		}


		public override void checkExists(Context context)
		{
			// nothing to do
		}


		public override bool isMoreSpecificThan(Context context, IType other)
		{
			return false;
		}

		public override Comparer<IValue> getComparer(Context context, IExpression key, bool descending)
		{
			if (key == null)
				return getNativeComparer(descending);
			else
				return getExpressionComparer(context, key, descending);
		}

		public virtual Comparer< IValue> getNativeComparer(bool descending)
		{
			throw new Exception("Missing native comparer for " + this.GetTypeName() + "!");
		}


		private Comparer<IValue> getExpressionComparer(Context context, IExpression key, bool descending)
		{
			if(key is ArrowExpression)
				return ((ArrowExpression)key).getNativeComparer(context, this, descending);
			else
				throw new Exception("Comparing native types with non-arrow key is not supported!");
		}

	}

}