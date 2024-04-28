using UnityEngine;
using Gley.GameServices;

public class GPAchievements : MonoBehaviour
{
    public void LoseAchievements(int lossCounter)
    {
        if (lossCounter >= 1000 && !Gley.GameServices.API.IsComplete(AchievementNames.AThousandBrokenPuzzles)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.AThousandBrokenPuzzles);
        
        if (lossCounter >= 750 && !Gley.GameServices.API.IsComplete(AchievementNames.IncredibleEndurance)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.IncredibleEndurance);

        if (lossCounter >= 500 && !Gley.GameServices.API.IsComplete(AchievementNames.ProgressivePath)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.ProgressivePath);

        if (lossCounter >= 250 && !Gley.GameServices.API.IsComplete(AchievementNames.Determination)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.Determination);
        
        if (lossCounter >= 100 && !Gley.GameServices.API.IsComplete(AchievementNames.Persistence)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.Persistence);

        if (lossCounter >= 50 && !Gley.GameServices.API.IsComplete(AchievementNames.AspiringImprovement)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.AspiringImprovement);
        
        if (lossCounter >= 10 && !Gley.GameServices.API.IsComplete(AchievementNames.InitialSteps))
            Gley.GameServices.API.SubmitAchievement(AchievementNames.InitialSteps);

        if (lossCounter >= 1 && !Gley.GameServices.API.IsComplete(AchievementNames.FirstLesson)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.FirstLesson);
    }

    public void ScoreAchievements(int score)
    {
        if (score >= 100000 && !Gley.GameServices.API.IsComplete(AchievementNames.MasteryofMastery)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.MasteryofMastery);

        if (score >= 75000 && !Gley.GameServices.API.IsComplete(AchievementNames.Unmatched)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.Unmatched);

        if (score >= 50000 && !Gley.GameServices.API.IsComplete(AchievementNames.WorldRecord))
            Gley.GameServices.API.SubmitAchievement(AchievementNames.WorldRecord);

        if (score >= 30000 && !Gley.GameServices.API.IsComplete(AchievementNames.PuzzleLegend))
            Gley.GameServices.API.SubmitAchievement(AchievementNames.PuzzleLegend);

        if (score >= 20000 && !Gley.GameServices.API.IsComplete(AchievementNames.MasterySummit)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.MasterySummit);

        if (score >= 15000 && !Gley.GameServices.API.IsComplete(AchievementNames.PuzzleChampion)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.PuzzleChampion);

        if (score >= 10000 && !Gley.GameServices.API.IsComplete(AchievementNames.ExpertLevel)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.ExpertLevel);
        
        if (score >= 5000 && !Gley.GameServices.API.IsComplete(AchievementNames.PuzzleMaster)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.PuzzleMaster);

        if (score >= 4000 && !Gley.GameServices.API.IsComplete(AchievementNames.BrilliantMind)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.BrilliantMind);

        if (score >= 3000 && !Gley.GameServices.API.IsComplete(AchievementNames.PathtoMastery)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.PathtoMastery);

        if (score >= 2000 && !Gley.GameServices.API.IsComplete(AchievementNames.AdvancedPlayer)) 
            Gley.GameServices.API.SubmitAchievement(AchievementNames.AdvancedPlayer);   

        if (score >= 1000 && !Gley.GameServices.API.IsComplete(AchievementNames.StartingPoint))
            Gley.GameServices.API.SubmitAchievement(AchievementNames.StartingPoint);
    }
}
