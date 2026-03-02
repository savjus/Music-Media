
public class ArtistService
{
    private static readonly List<Artist> _data = new()
    {
        new Artist { Id = 1,  Name = "Andrius Mamontovas", Genre = "Rock",        Language = "Lithuanian",  ActiveFrom = 1989,                Country = "Lithuania"  },
        new Artist { Id = 2,  Name = "Skamp",              Genre = "Pop",         Language = "English",   ActiveFrom = 1999, ActiveTo = 2005, Country = "Lithuania"  },
        new Artist { Id = 3,  Name = "Jurga",              Genre = "Pop",         Language = "Lithuanian",  ActiveFrom = 2001,                Country = "Lithuania"  },
        new Artist { Id = 4,  Name = "The Beatles",        Genre = "Rock",        Language = "English",   ActiveFrom = 1960, ActiveTo = 1970, Country = "UK"       },
        new Artist { Id = 5,  Name = "Radiohead",          Genre = "Alternative", Language = "English",   ActiveFrom = 1985,                Country = "UK"       },
        new Artist { Id = 6,  Name = "Björk",              Genre = "Electronic",  Language = "English",   ActiveFrom = 1986,                Country = "Iceland"  },
        new Artist { Id = 7,  Name = "Coldplay",           Genre = "Pop",         Language = "English",   ActiveFrom = 1996,                Country = "UK"       },
        new Artist { Id = 8,  Name = "Stromae",            Genre = "Electronic",  Language = "French",  ActiveFrom = 2009,                Country = "Belgium"  },
        new Artist { Id = 9,  Name = "Rammstein",          Genre = "Metal",       Language = "German",  ActiveFrom = 1994,                Country = "Germany"  },
        new Artist { Id = 10, Name = "Daft Punk",          Genre = "Electronic",  Language = "English",   ActiveFrom = 1993, ActiveTo = 2021, Country = "France"   },
        new Artist { Id = 11, Name = "Beyoncé",            Genre = "R&B",         Language = "English",   ActiveFrom = 1997,                Country = "USA"      },
        new Artist { Id = 12, Name = "Kendrick Lamar",     Genre = "Hip-Hop",     Language = "English",   ActiveFrom = 2003,                Country = "USA"      },
        new Artist { Id = 13, Name = "G&G Sindikatas",     Genre = "Hip-Hop",     Language = "Lithuanian",  ActiveFrom = 1994,                Country = "Lithuania"  },
        new Artist { Id = 14, Name = "Sigur Rós",          Genre = "Post-Rock",   Language = "Icelandic",   ActiveFrom = 1994,                Country = "Iceland"  },
        new Artist { Id = 15, Name = "Seu Jorge",          Genre = "MPB",         Language = "Portuguese", ActiveFrom = 1998,                Country = "Brazil"   },
    };

    public List<Artist> Search(string? name, List<string>? genres, List<string>? languages, int? yearFrom, int? yearTo, bool onlyActive)
    {
        var q = _data.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            q = q.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (genres != null && genres.Count > 0)
        {
            q = q.Where(a => genres.Contains(a.Genre));
        }

        if (languages != null && languages.Count > 0)
        {
            q = q.Where(a => languages.Contains(a.Language));
        }

        if (yearFrom.HasValue)
        {
            q = q.Where(a => a.ActiveTo == null || a.ActiveTo >= yearFrom);
        }

        if (yearTo.HasValue)
        {
            q = q.Where(a => a.ActiveFrom <= yearTo);
        }

        if (onlyActive)
        {
            q = q.Where(a => a.ActiveTo == null);
        }

        return q.OrderBy(a => a.Name).ToList();
    }

    public List<string> GetGenres()
    {
        return _data.Select(a => a.Genre).Distinct().Order().ToList();
    }

    public List<string> GetLanguages()
    {
        return _data.Select(a => a.Language).Distinct().Order().ToList();
    }

    public Artist? GetById(int id) 
    {
        return _data.FirstOrDefault(a => a.Id == id);
    }
}