using KOF.Data;
using KOF.Data.Models;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Models
{
    public class QuestTargetNpc
    {
        public int ProtoId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; } = "";
        public int NpcX { get; set; }
        public int NpcY { get; set; }
        public int NpcZone { get; set; }
        public int NeededKillCount { get; set; }
        public int KillCount { get; set; }
    }

    public class QuestCollectItem
    {
        public int ItemId { get; set; }
        public int CollectableNpcProtoId { get; set; }
        public string CollectableNpcName { get; set; } = "";
        public int CollectableNpcX { get; set; }
        public int CollectableNpcY { get; set; }
        public int CollectableNpcZone { get; set; }
        public int NeededItemCount { get; set; }
        public int ItemCount { get; set; }

        public Vector3 GetCollectableNpcPosition()
        {
            return new Vector3(CollectableNpcX, CollectableNpcY, 0.0f);
        }
    }

    public class Quest
    {
        public int Id { get; set; }
        public int BaseId { get; set; }
        public string LuaName { get; set; } = "";
        public string Title { get; set; } = "";
        [Browsable(false)]
        public string Description { get; set; } = "";
        public int MinLevel { get; set; }
        public int NpcProtoId { get; set; }
        public int NpcZone { get; set; }
        public int NpcX { get; set; }
        public int NpcY { get; set; }
        public int Status { get; set; }
        public QuestTargetNpc[] TargetNpc { get; set; } = default!;
        public QuestCollectItem[] CollectItem { get; set; } = default!;
        //public int[,] NeededItem { get; set; } = default!;
        public int[,] RewardItem { get; set; } = default!;

        public Quest()
        {
            //TargetNpc = new QuestTargetNpc[4];
        }

        public void Build(Character character)
        {
            var questHelper = TableHandler.GetQuestHelperList()
                                .FirstOrDefault(x => x.EventDataIndex == Id &&
                                                        (x.EventStatus == Status || x.EventStatus == 4) &&
                                                        (x.Class == 5 || x.Class == Character.GetRepresentClass(character.Class)) &&
                                                        (x.Nation == character.NationId || x.Nation == 3));

            if (questHelper != null)
            {
                var questGuide = TableHandler.GetQuestGuideList().FirstOrDefault(x => x.Id == questHelper.GuideIndex);

                if (questGuide != null)
                {
                    Title = questGuide.Title;
                    Description = questGuide.Description;
                    MinLevel = questGuide.MinLevel;
                }

                var questMonsterExchange = TableHandler.GetQuestMonsterExchangeList().FirstOrDefault(x => x.Id == Id);

                if (questMonsterExchange != null)
                {
                    TargetNpc = new QuestTargetNpc[4];

                    var targetNpcProto = questMonsterExchange.TargetNpcProto;

                    for (int i = 0; i < 4; i++)
                    {
                        TargetNpc[i] = new QuestTargetNpc()
                        {
                            ProtoId = targetNpcProto[i, 0],
                            NeededKillCount = targetNpcProto[i, 1]
                        };

                        var targetNpcDesc = TableHandler.GetQuestNpcDescList().FirstOrDefault(x => x.NpcProtoId == TargetNpc[i].ProtoId && x.Zone == questHelper.Zone);

                        if (targetNpcDesc != null)
                        {
                            TargetNpc[i].Type = targetNpcDesc.NpcType;
                            TargetNpc[i].Name = targetNpcDesc.Name;
                            TargetNpc[i].NpcX = targetNpcDesc.X;
                            TargetNpc[i].NpcY = targetNpcDesc.Y;
                            TargetNpc[i].NpcZone = targetNpcDesc.Zone;
                        }
                    }
                }

                BaseId = questHelper.BaseId;

                NpcProtoId = questHelper.NpcProtoId;

                var questNpcDesc = TableHandler.GetQuestNpcDescList().FirstOrDefault(x => x.NpcProtoId == questHelper.NpcProtoId && x.Zone == questHelper.Zone);

                if (questNpcDesc != null)
                {
                    NpcX = questNpcDesc.X;
                    NpcY = questNpcDesc.Y;
                    NpcZone = questNpcDesc.Zone;
                }

                LuaName = questHelper.LuaName;

                BuildNeededItemList(questHelper.ItemExchangeIndex);
            }
        }

        private void BuildNeededItemList(int exchangeIndex)
        {
            var questItemExchange = TableHandler.GetItemExchangeList().FirstOrDefault(x => x.Id == exchangeIndex);

            if (questItemExchange == null) return;

            CollectItem = new QuestCollectItem[5];

            for (int i = 0; i < 5; i++)
            {
                CollectItem[i] = new QuestCollectItem()
                {
                    ItemId = questItemExchange.NeededItem[i, 0],
                    NeededItemCount = questItemExchange.NeededItem[i, 1]
                };

                var questItemDesc = TableHandler.GetQuestItemDescList().FirstOrDefault(x => x.ItemId == CollectItem[i].ItemId);

                if (questItemDesc == null) continue;

                var questNpcDesc = TableHandler.GetQuestNpcDescList().FirstOrDefault(x => x.Id == questItemDesc.NpcDescIndex);

                if (questNpcDesc != null)
                {
                    CollectItem[i].CollectableNpcProtoId = questNpcDesc.NpcProtoId;
                    CollectItem[i].CollectableNpcX = questNpcDesc.X;
                    CollectItem[i].CollectableNpcY = questNpcDesc.Y;
                    CollectItem[i].CollectableNpcZone = questNpcDesc.Zone;
                    CollectItem[i].CollectableNpcName = questNpcDesc.Name;
                }
            }
        }

        public Vector3 GetQuestNpcPosition()
        {
            return new Vector3(NpcX, NpcY, 0.0f);
        }

        public bool IsQuestCompleted()
        {
            if (Status == 2) 
                return true;

            if (IsKillCompleted() && IsCollectCompleted())
                return true;

            return false;
        }

        public bool IsKillCompleted()
        {
            bool killCompleted = true;

            if(TargetNpc != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (TargetNpc[i].KillCount < TargetNpc[i].NeededKillCount)
                        killCompleted = false;
                }
            }

            return killCompleted;
        }

        public bool IsCollectCompleted()
        {
            bool killCompleted = true;

            if (CollectItem != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (CollectItem[i].ItemCount < CollectItem[i].NeededItemCount)
                        killCompleted = false;
                }
            }

            return killCompleted;
        }

        public void SetKillCount(int index, int count)
        {
            if (TargetNpc == null)
                return;

            TargetNpc[index].KillCount = count;
        }
    }
}
