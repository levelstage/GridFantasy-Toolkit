using GfEngine.Models.Actors;
using GfEngine.Models.Items;
using GfEngine.Logics;
using System.Collections.Generic;
namespace GfEngine.Campaigns
{
    public class PlayerState
    {
        public int GameSeed { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int NextLevelExperience { get; set; }
        public List<Actor> Roster { get; set; }
        public List<Item> PartyInventory { get; set; }
        public Dictionary<int, Dictionary<int, List<Trait>>> TraitDeck { get; private set; }

        public PlayerState()
        {
            Level = 1;
            Experience = 0;
            NextLevelExperience = 100;
            Roster = new List<Actor>();
            PartyInventory = new List<Item>();
            Gold = 0;
        }

        public void GenerateAllTraitDecks()
        {
            foreach (Actor character in GameData.AllActors.Values)
            {
                if (character.Code == 0) continue; // 팬텀은 특성 없음
                TraitDeck[character.Code] = TraitManager.GetTraitDeck(character, GameData.TraitLevels[0]);
            }
        }

        public void AddCharacterToRoster(Actor newCharacter)
        {
            Roster.Add(newCharacter);
        }
    }
}