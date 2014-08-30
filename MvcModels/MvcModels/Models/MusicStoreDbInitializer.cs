using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{//se agregan algunos datos para inicializar la base de datos.//borra y crea la base de datos si el modelo cambia.
    public class MusicStoreDbInitializer : DropCreateDatabaseIfModelChanges<MusicStoreDBContext>
    {
        protected override void Seed(MusicStoreDBContext context)
        {
            context.Artists.Add(new Artist { Name = "Al Di Meola" });
            context.Genres.Add(new Genre{Name="Jazz"});
            context.Albums.Add(new Album
            {
                Artist = new Artist { Name = "Rush" },
                Genre = new Genre { Name = "Rock" },
                Price = 9.99m,
                Title = "Caravan"
            });
            base.Seed(context);
        }
    }
}