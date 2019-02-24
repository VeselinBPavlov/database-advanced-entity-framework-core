namespace HelperClasses.Configurations
{
    public static class Util
    {
        public static string Error = "Connection with database is lost. Try again to connect!";
        public static string DBCreateSuccess = "Database created successfully!";
        public static string TableCreateSuccess = "Table created successfully!";
        public static string InsertStatementSuccess = "Insert successfull!";
        public static string ReadingDataSuccess = "Reading entity success!";

        public static string InsertTownSuccess = "Town {0} was added to the database.";
        public static string InsertVillainSuccess = "Villain {0} was added to the database.";
        public static string InsertMinionVillainSuccess = "Successfully added {0} to be minion of {1}.";

        public static string UpdateTownsSuccess = "{0} town names were affected.";
        public static string NoAffectedTowns = "No town names were affected.";

        public const string NoViillainFound = "No such villain was found.";
        public const string DeletedVillain = "{0} was deleted.";
        public const string ReleasedMinions = "{0} minions were released.";
    }
}
