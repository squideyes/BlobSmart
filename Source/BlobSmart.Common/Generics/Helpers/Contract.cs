namespace BlobSmart.Common.Generics
{
    public static class Contract
    {
        public static void Requires(bool isValid, string fieldName)
        {
            if (!isValid)
                throw new BadFieldException(fieldName);
        }
    }
}
