using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication.Data.Kintai
{

    public partial class ApplicationDbContextKintai : DbContext
    {

        public virtual DbSet<V_TOTAL_WORKING_TIME> V_TOTAL_WORKING_TIMEs { get; set; }

    }

}
