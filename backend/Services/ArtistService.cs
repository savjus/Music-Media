
public class ArtistService
{
    private static readonly List<Artist> _data = new()
    {
        new Artist { Id = 101, Name = "Andrius Mamontovas", Genre = "Rock",        Language = "Lithuanian", ActiveFrom = 1989,                Country = "Lithuania" },
        new Artist { Id = 102, Name = "Skamp",              Genre = "Pop",         Language = "English",    ActiveFrom = 1999, ActiveTo = 2005, Country = "Lithuania" },
        new Artist { Id = 103, Name = "Jurga",              Genre = "Pop",         Language = "Lithuanian", ActiveFrom = 2001,                Country = "Lithuania" },
        new Artist { Id = 104, Name = "The Beatles",        Genre = "Rock",        Language = "English",    ActiveFrom = 1960, ActiveTo = 1970, Country = "UK" },
        new Artist { Id = 105, Name = "Radiohead",          Genre = "Alternative", Language = "English",    ActiveFrom = 1985,                Country = "UK" },
        new Artist { Id = 106, Name = "Björk",              Genre = "Electronic",  Language = "English",    ActiveFrom = 1986,                Country = "Iceland" },
        new Artist { Id = 107, Name = "Coldplay",           Genre = "Pop",         Language = "English",    ActiveFrom = 1996,                Country = "UK" },
        new Artist { Id = 108, Name = "Stromae",            Genre = "Electronic",  Language = "French",     ActiveFrom = 2009,                Country = "Belgium" },
        new Artist { Id = 109, Name = "Rammstein",          Genre = "Metal",       Language = "German",     ActiveFrom = 1994,                Country = "Germany" },
        new Artist { Id = 110, Name = "Daft Punk",          Genre = "Electronic",  Language = "English",    ActiveFrom = 1993, ActiveTo = 2021, Country = "France" },
        new Artist { Id = 111, Name = "Beyoncé",            Genre = "R&B",         Language = "English",    ActiveFrom = 1997,                Country = "USA" },
        new Artist { Id = 112, Name = "Kendrick Lamar",     Genre = "Hip-Hop",     Language = "English",    ActiveFrom = 2003,                Country = "USA" },
        new Artist { Id = 113, Name = "G&G Sindikatas",     Genre = "Hip-Hop",     Language = "Lithuanian", ActiveFrom = 1994,                Country = "Lithuania" },
        new Artist { Id = 114, Name = "Sigur Rós",          Genre = "Post-Rock",   Language = "Icelandic",  ActiveFrom = 1994,                Country = "Iceland" },
        new Artist { Id = 115, Name = "Seu Jorge",          Genre = "MPB",         Language = "Portuguese", ActiveFrom = 1998,                Country = "Brazil" },

        new Artist { Id = 116, Name = "Adele",              Genre = "Pop",         Language = "English",    ActiveFrom = 2006,                Country = "UK" },
        new Artist { Id = 117, Name = "Taylor Swift",       Genre = "Pop",         Language = "English",    ActiveFrom = 2004,                Country = "USA" },
        new Artist { Id = 118, Name = "Metallica",          Genre = "Metal",       Language = "English",    ActiveFrom = 1981,                Country = "USA" },
        new Artist { Id = 119, Name = "Nirvana",            Genre = "Rock",        Language = "English",    ActiveFrom = 1987, ActiveTo = 1994, Country = "USA" },
        new Artist { Id = 120, Name = "Arctic Monkeys",     Genre = "Alternative", Language = "English",    ActiveFrom = 2002,                Country = "UK" },
        new Artist { Id = 121, Name = "The Weeknd",         Genre = "R&B",         Language = "English",    ActiveFrom = 2010,                Country = "Canada" },
        new Artist { Id = 122, Name = "Drake",              Genre = "Hip-Hop",     Language = "English",    ActiveFrom = 2001,                Country = "Canada" },
        new Artist { Id = 123, Name = "M83",                Genre = "Electronic",  Language = "English",    ActiveFrom = 2001,                Country = "France" },
        new Artist { Id = 124, Name = "Phoenix",            Genre = "Alternative", Language = "English",    ActiveFrom = 1995,                Country = "France" },
        new Artist { Id = 125, Name = "ABBA",               Genre = "Pop",         Language = "English",    ActiveFrom = 1972, ActiveTo = 1982, Country = "Sweden" },
        new Artist { Id = 126, Name = "Robyn",              Genre = "Pop",         Language = "English",    ActiveFrom = 1991,                Country = "Sweden" },
        new Artist { Id = 127, Name = "Tame Impala",        Genre = "Alternative", Language = "English",    ActiveFrom = 2007,                Country = "Australia" },
        new Artist { Id = 128, Name = "AC/DC",              Genre = "Rock",        Language = "English",    ActiveFrom = 1973,                Country = "Australia" },
        new Artist { Id = 129, Name = "Rosalía",            Genre = "Pop",         Language = "Spanish",    ActiveFrom = 2013,                Country = "Spain" },
        new Artist { Id = 130, Name = "Bad Bunny",          Genre = "Hip-Hop",     Language = "Spanish",    ActiveFrom = 2013,                Country = "Puerto Rico" },
        new Artist { Id = 131, Name = "Kraftwerk",          Genre = "Electronic",  Language = "German",     ActiveFrom = 1970,                Country = "Germany" },
        new Artist { Id = 132, Name = "Måneskin",           Genre = "Rock",        Language = "Italian",    ActiveFrom = 2016,                Country = "Italy" },
        new Artist { Id = 133, Name = "Soda Stereo",        Genre = "Rock",        Language = "Spanish",    ActiveFrom = 1982, ActiveTo = 1997, Country = "Argentina" },
        new Artist { Id = 134, Name = "Fela Kuti",          Genre = "Afrobeat",    Language = "English",    ActiveFrom = 1958, ActiveTo = 1997, Country = "Nigeria" },
        new Artist { Id = 135, Name = "Angèle",             Genre = "Pop",         Language = "French",     ActiveFrom = 2015,                Country = "Belgium" },
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
        var artist = _data.FirstOrDefault(a => a.Id == id);

        if (artist is not null)
        {
            artist.ProfileViews++;
        }

        return artist;
    }

    public Artist? FindSimilarArtist(List<int> artistIds)
    {
        if (artistIds == null || artistIds.Count == 0)
            return null;

        var selectedArtists = _data.Where(a => artistIds.Contains(a.Id)).ToList();
        if (selectedArtists.Count == 0)
            return null;

        var selectedGenres = selectedArtists.Select(a => a.Genre).Distinct().ToHashSet();
        
        var candidates = _data
            .Where(a => !artistIds.Contains(a.Id))
            .ToList();

        if (candidates.Count == 0)
            return null;

        var bestMatch = candidates
            .MaxBy(a => selectedGenres.Count(g => g == a.Genre));

        return bestMatch;
    }
}