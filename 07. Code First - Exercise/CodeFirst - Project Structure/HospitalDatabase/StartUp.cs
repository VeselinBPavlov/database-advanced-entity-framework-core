namespace P01_HospitalDatabase
{
    using Data;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var db = new HospitalContext();
            db.Database.EnsureCreated();
        }
    }
}
