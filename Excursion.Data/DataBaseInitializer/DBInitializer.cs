using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Excursion.Data.DataBaseInitializer
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ExcursionContext>
    {


        public DBInitializer(ExcursionContext context)
        {
            this.Seed(context);
        }


        protected override void Seed(ExcursionContext context)
        {

            var users = new List<Region>
           {
               new Region{ }
           };
            users.ForEach(s => context.RegionSet.Add(s));
            context.SaveChanges();
            



        }



    }
}