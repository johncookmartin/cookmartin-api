-- 98th Academy Awards (2026) Seed Script
-- Ceremony: March 15, 2026 | Host: Conan O'Brien
-- Wrapped in an existence check so it only runs on a fresh database

IF NOT EXISTS (SELECT 1 FROM [oscar].[Categories])
BEGIN

    SET IDENTITY_INSERT [oscar].[Categories] ON;

    INSERT INTO [oscar].[Categories] ([CategoryId], [Name], [DisplayOrder]) VALUES
    (1,  'Best Picture',                24),
    (2,  'Best Director',               22),
    (3,  'Best Actor',                  21),
    (4,  'Best Actress',                23),
    (5,  'Best Supporting Actor',       1),
    (6,  'Best Supporting Actress',     9),
    (7,  'Best Animated Feature',       2),
    (8,  'Best International Feature',  18),
    (9,  'Best Documentary Feature',    13),
    (10, 'Best Original Screenplay',    5),
    (11, 'Best Adapted Screenplay',     6),
    (12, 'Best Cinematography',         17),
    (13, 'Best Film Editing',           8),
    (14, 'Best Original Score',         20),
    (15, 'Best Original Song',          11),
    (16, 'Best Production Design',      10),
    (17, 'Best Costume Design',         4),
    (18, 'Best Makeup and Hairstyling', 7),
    (19, 'Best Visual Effects',         15),
    (20, 'Best Sound',                  14),
    (21, 'Best Live Action Short Film', 16),
    (22, 'Best Documentary Short Film', 12),
    (23, 'Best Casting',                19),
    (24, 'Best Animated Short Film',    3);

    SET IDENTITY_INSERT [oscar].[Categories] OFF;

    -- Best Picture
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (1, 'Bugonia'),
    (1, 'F1'),
    (1, 'Frankenstein'),
    (1, 'Hamnet'),
    (1, 'Marty Supreme'),
    (1, 'One Battle After Another'),
    (1, 'The Secret Agent'),
    (1, 'Sentimental Value'),
    (1, 'Sinners'),
    (1, 'Train Dreams');

    -- Best Director
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (2, 'Ryan Coogler — Sinners'),
    (2, 'Chloé Zhao — Hamnet'),
    (2, 'Paul Thomas Anderson — One Battle After Another'),
    (2, 'Josh Safdie — Marty Supreme'),
    (2, 'Joachim Trier — Sentimental Value');

    -- Best Actor
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (3, 'Timothée Chalamet — Marty Supreme'),
    (3, 'Leonardo DiCaprio — One Battle After Another'),
    (3, 'Ethan Hawke — Blue Moon'),
    (3, 'Michael B. Jordan — Sinners'),
    (3, 'Wagner Moura — The Secret Agent');

    -- Best Actress
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (4, 'Jessie Buckley — Hamnet'),
    (4, 'Rose Byrne — If I Had Legs I''d Kick You'),
    (4, 'Kate Hudson — Song Sung Blue'),
    (4, 'Renate Reinsve — Sentimental Value'),
    (4, 'Emma Stone — Bugonia');

    -- Best Supporting Actor
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (5, 'Benicio Del Toro — One Battle After Another'),
    (5, 'Jacob Elordi — Frankenstein'),
    (5, 'Delroy Lindo — Sinners'),
    (5, 'Sean Penn — One Battle After Another'),
    (5, 'Stellan Skarsgård — Sentimental Value');

    -- Best Supporting Actress
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (6, 'Elle Fanning — Sentimental Value'),
    (6, 'Inga Ibsdotter Lilleaas — Sentimental Value'),
    (6, 'Amy Madigan — Weapons'),
    (6, 'Wunmi Mosaku — Sinners'),
    (6, 'Teyana Taylor — One Battle After Another');

    -- Best Animated Feature
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (7, 'Arco'),
    (7, 'Elio'),
    (7, 'KPop Demon Hunters'),
    (7, 'Little Amélie or the Character of Rain'),
    (7, 'Zootopia 2');

    -- Best International Feature
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (8, 'It Was Just an Accident (France)'),
    (8, 'The Secret Agent'),
    (8, 'Sentimental Value (Norway)'),
    (8, 'Sirât (Spain)'),
    (8, 'The Voice of Hind Rajab (Tunisia)');

    -- Best Documentary Feature
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (9, 'The Alabama Solution'),
    (9, 'Come See Me in the Good Light'),
    (9, 'Cutting Through Rocks'),
    (9, 'Mr. Nobody Against Putin'),
    (9, 'The Perfect Neighbor');

    -- Best Original Screenplay
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (10, 'Blue Moon'),
    (10, 'It Was Just an Accident'),
    (10, 'Marty Supreme'),
    (10, 'Sentimental Value'),
    (10, 'Sinners');

    -- Best Adapted Screenplay
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (11, 'Bugonia'),
    (11, 'Frankenstein'),
    (11, 'Hamnet'),
    (11, 'One Battle After Another'),
    (11, 'Train Dreams');

    -- Best Cinematography
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (12, 'Dan Laustsen — Frankenstein'),
    (12, 'Darius Khondji — Marty Supreme'),
    (12, 'Michael Bauman — One Battle After Another'),
    (12, 'Autumn Durald Arkapaw — Sinners'),
    (12, 'Adolpho Veloso — Train Dreams');

    -- Best Film Editing
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (13, 'Stephen Mirrione — F1'),
    (13, 'Ronald Bronstein & Josh Safdie — Marty Supreme'),
    (13, 'Andy Jurgensen — One Battle After Another'),
    (13, 'Olivier Bugge Coutté — Sentimental Value'),
    (13, 'Michael P. Shawver — Sinners');

    -- Best Original Score
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (14, 'Jerskin Fendrix — Bugonia'),
    (14, 'Alexandre Desplat — Frankenstein'),
    (14, 'Max Richter — Hamnet'),
    (14, 'Jonny Greenwood — One Battle After Another'),
    (14, 'Ludwig Göransson — Sinners');

    -- Best Original Song
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (15, '"Dear Me" — Diane Warren: Relentless'),
    (15, '"Golden" — KPop Demon Hunters'),
    (15, '"I Lied to You" — Sinners'),
    (15, '"Sweet Dreams of Joy" — Viva Verdi!'),
    (15, '"Train Dreams" — Train Dreams');

    -- Best Production Design
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (16, 'Frankenstein'),
    (16, 'Hamnet'),
    (16, 'Marty Supreme'),
    (16, 'One Battle After Another'),
    (16, 'Sinners');

    -- Best Costume Design
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (17, 'Deborah L. Scott — Avatar: Fire and Ash'),
    (17, 'Kate Hawley — Frankenstein'),
    (17, 'Malgosia Turzanska — Hamnet'),
    (17, 'Miyako Bellizzi — Marty Supreme'),
    (17, 'Ruth E. Carter — Sinners');

    -- Best Makeup and Hairstyling
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (18, 'Frankenstein'),
    (18, 'Kokuho'),
    (18, 'Sinners'),
    (18, 'The Smashing Machine'),
    (18, 'The Ugly Stepsister');

    -- Best Visual Effects
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (19, 'Avatar: Fire and Ash'),
    (19, 'F1'),
    (19, 'Jurassic World Rebirth'),
    (19, 'The Lost Bus'),
    (19, 'Sinners');

    -- Best Sound
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (20, 'F1'),
    (20, 'Frankenstein'),
    (20, 'One Battle After Another'),
    (20, 'Sinners'),
    (20, 'Sirât');

    -- Live Action Short Film
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (21, 'Butcher''s Stain'),
    (21, 'A Friend of Dorothy'),
    (21, 'Jane Austen''s Period Drama'),
    (21, 'The Singers'),
    (21, 'Two People Exchanging Saliva');

    -- Documentary Short Film
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (22, 'All the Empty Rooms'),
    (22, 'Armed Only With a Camera: The Life and Death of Brent Renaud'),
    (22, 'Children No More: ''Were and are Gone'''),
    (22, 'The Devil is Busy'),
    (22, 'Perfectly a Strangeness');

    -- Casting
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (23, 'Hamnet'),
    (23, 'Marty Supreme'),
    (23, 'One Battle After Another'),
    (23, 'The Secret Agent'),
    (23, 'Sinners');

    -- Animated Short Film
    INSERT INTO [oscar].[Nominees] ([CategoryId], [Name]) VALUES
    (24, 'Butterfly'),
    (24, 'Forevergreen'),
    (24, 'The Girl Who Cried Pearls'),
    (24, 'Retirement Plan'),
    (24, 'The Three Sisters');


END
