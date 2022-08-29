PRAGMA foreign_keys = false;
UPDATE "Zone" SET "Smd" = 'in_dungeon01.smd', "Opd" = 'in_dungeon01.opd', "Gtd" = 'in_dungeon01.gtd' WHERE rowid = 81;
UPDATE "Zone" SET "Smd" = 'in_dungeon02.smd', "Opd" = 'in_dungeon02.opd', "Gtd" = 'in_dungeon02.gtd' WHERE rowid = 82;
UPDATE "Zone" SET "Smd" = 'in_dungeon03.smd', "Opd" = 'in_dungeon03.opd', "Gtd" = 'in_dungeon03.gtd' WHERE rowid = 83;
PRAGMA foreign_keys = true;