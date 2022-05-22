namespace Service.Server
{
    [System.Serializable]
    public struct FormattedDataRequest
    {
        public string date;

        public static FormattedDataRequest GetFormattedDataRequest(int day, int month, int year)
        {
            return new FormattedDataRequest() { date = $"{day}-{month}-{year}" };
        }
    }
}