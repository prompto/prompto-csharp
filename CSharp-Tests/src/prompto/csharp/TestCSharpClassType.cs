using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Reflection;
using prompto.runtime;
using prompto.type;
using prompto.declaration;

namespace prompto.csharp
{
	[TestFixture]
	public class TestCSharpClassType
	{
		public List<AttributeDeclaration> getAllAttributes() {
			return null;
		}

		[Test]
		public void TestAttributeList ()
		{
			MethodInfo method = typeof(TestCSharpClassType).GetMethod("getAllAttributes");
			Assert.IsNotNull(method);
			Type type = method.ReturnType;
			Assert.IsNotNull(type);
			Context context = Context.newGlobalContext();
			String name = "Attribute";
			NativeCategoryDeclaration declaration = new NativeCategoryDeclaration(name, null, null, null, null);
			context.registerNativeBinding(typeof(AttributeDeclaration), declaration);
			IType listType = new ListType(new CategoryType(name));
			IType returnType = new CSharpClassType(type).ConvertCSharpTypeToPromptoType(context, listType);
			Assert.AreEqual(listType, returnType);
		}
	}
}

