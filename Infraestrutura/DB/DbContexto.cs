using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal.Dominios.Entidades;

namespace minimal.Infraestrutura.DB
{
    public class DbContexto : DbContext
    {
        private readonly IConfiguration _configuracaoAppSetting;
        public DbContexto(IConfiguration configuration)
        {
            _configuracaoAppSetting = configuration;
        }
        public DbSet<Adiminstrador> Adiminstradors { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConecao = _configuracaoAppSetting.GetConnectionString("mysql")?.ToString();
            if (stringConecao != null && stringConecao != "")
            {
                optionsBuilder.UseMySql(stringConecao,
                 ServerVersion.AutoDetect(stringConecao));
            }
        }
    }
}