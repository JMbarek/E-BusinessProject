namespace Excursion.Data
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity;
    #endregion
    public class ExcursionContext : DbContext
    {
        #region Proprietes
      
        public DbSet<Excursion> ExcursionSet { get; set; }
        public DbSet<Excursionn> ExcursionnSet { get; set; }
        public DbSet<HistoricPayement> HistoricPayementSet { get; set; }
        public DbSet<Hotel> HotelSet { get; set; }
        public DbSet<Jour> JourSet { get; set; }
        public DbSet<Langue> LangueSet { get; set; }
        public DbSet<Periode> PeriodeSet { get; set; }
        public DbSet<Prix> PrixSet { get; set; }
        public DbSet<Region> RegionSet { get; set; }
        public DbSet<Reservation> ReservationSet { get; set; }
        public DbSet<ReservationValide> ReservationValideSet { get; set; }
        public DbSet<SortieParSemaine> SortieParSemaineSet { get; set; }
        public DbSet<TypeExc> TypeExcSet { get; set; }
        public DbSet<User> UserSet { get; set; }
        public DbSet<Zone> ZoneSet { get; set; }
        public DbSet<Centre> CentreSet { get; set; }
        public DbSet<Theme> ThemeSet { get; set; }
        public DbSet<ExcursionTheme> ExcursionThemeSet { get; set; }
        public DbSet<TourOp> TourOpSet { get; set; }
        public DbSet<ZonePrix> ZonePrixSet { get; set; }
        public DbSet<Cart> CartSet { get; set; }
        public DbSet<Order> OrderSet { get; set; }
        public DbSet<OrderDetail> OrderDetailSet { get; set; }

        #endregion
        public ExcursionContext()
            : base("name=ExcursionContext")
        {
        }
    }
}
