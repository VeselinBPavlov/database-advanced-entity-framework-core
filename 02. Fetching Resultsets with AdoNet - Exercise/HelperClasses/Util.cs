namespace HelperClasses
{
    public static class Util
    {
        public const string Error = "Connection with database is lost. Try again to connect!";
        public const string DBCreateSuccess = "Database created successfully!";
        public const string TableCreateSuccess = "Table created successfully!";
        public const string InsertStatementSuccess = "Insert successfull!";
        public const string ReadingDataSuccess = "Reading entity success!";

        public const string InsertTownSuccess = "Town {0} was added to the database.";
        public const string InsertVillainSuccess = "Villain {0} was added to the database.";
        public const string InsertMinionVillainSuccess = "Successfully added {0} to be minion of {1}.";

        public const string UpdateTownsSuccess = "{0} town names were affected.";
        public const string NoAffectedTowns = "No town names were affected.";

        public const string NoViillainFound = "No such villain was found.";
        public const string DeletedVillain = "{0} was deleted.";
        public const string ReleasedMinions = "{0} minions were released.";
    }
}
