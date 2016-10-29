namespace prompto.grammar
{

    public enum ContOp
    {
        IN,
        CONTAINS,
        CONTAINS_ALL,
        CONTAINS_ANY,
        NOT_IN,
        NOT_CONTAINS,
        NOT_CONTAINS_ALL,
        NOT_CONTAINS_ANY
    }

	public static class ContOpMethods
	{
		public static ContOp? reverse(this ContOp match)
		{
			switch (match)
			{
				case ContOp.IN:
					return ContOp.CONTAINS;
				case ContOp.CONTAINS:
					return ContOp.IN;
				case ContOp.NOT_IN:
					return ContOp.NOT_CONTAINS;
				case ContOp.NOT_CONTAINS:
					return ContOp.NOT_IN;
				default:
					return null;
			}
		}
	}

}