namespace prompto.grammar
{

    public enum ContOp
    {
        IN,
        HAS,
        HAS_ALL,
        HAS_ANY,
        NOT_IN,
        NOT_HAS,
        NOT_HAS_ALL,
        NOT_HAS_ANY
    }

	public static class ContOpMethods
	{
		public static ContOp? reverse(this ContOp match)
		{
			switch (match)
			{
				case ContOp.IN:
					return ContOp.HAS;
				case ContOp.HAS:
					return ContOp.IN;
				case ContOp.NOT_IN:
					return ContOp.NOT_HAS;
				case ContOp.NOT_HAS:
					return ContOp.NOT_IN;
				default:
					return null;
			}
		}
	}

}