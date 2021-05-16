using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Achievements : MonoBehaviour
{
    public static void Progress(AchievementType type, float amount)
    {
        var achievements = AllAchievements.Where(x => x.Type == type);
        if(achievements != null)
        {
            foreach(var achievement in achievements)
            {
                achievement.Amount += amount;
                if (achievement.Amount >= achievement.Target && !achievement.Achieved)
                {
                    // Completed achievement
                    achievement.Achieved = true;

                    // Basically if testing
                    if(AchievementManager.Instance != null)
                        AchievementManager.Instance.DisplayAchievement(achievement);
                }
            }
        }
    }

    [System.Serializable]
    public class Achievement
    {
        public string Title;
        public string Description;
        public AchievementType Type;
        public float Target;
        public float Amount;
        public bool Achieved;
    }

    public enum AchievementType { Shoot, Walk, FastShoot, UseLaser}

    public static List<Achievement> AllAchievements = new List<Achievement>
    {
    #region SHOOTING
        new Achievement()
        {
            Title = "Shoot 10",
            Description = "Shoot 10 times",
            Type = AchievementType.Shoot,
            Target = 10,
            Amount = 0,
            Achieved = false
        },
        new Achievement()
        {
            Title = "Shoot 50",
            Description = "Shoot 50 times",
            Type = AchievementType.Shoot,
            Target = 50,
            Amount = 0,
            Achieved = false
        },
        new Achievement()
        {
            Title = "Shoot 500",
            Description = "Shoot 500 times",
            Type = AchievementType.Shoot,
            Target = 50,
            Amount = 0,
            Achieved = false
        }
	#endregion
        ,
    #region WALKING
        new Achievement()
        {
            Title = "Walk 10",
            Description = "Walk for 10 seconds",
            Type = AchievementType.Walk,
            Target = 10,
            Amount = 0,
            Achieved = false
        }
	#endregion
        ,
    #region BUGS
        new Achievement()
        {
            Title = "BUG",
            Description = "Fast shoot by shooting again before the shot delay has ran out",
            Type = AchievementType.FastShoot,
            Target = 1,
            Amount = 0,
            Achieved = false
        }
	#endregion
        ,
    #region OTHER
        new Achievement()
        {
            Title = "Use laser",
            Description = "Use the very much broken laser",
            Type = AchievementType.UseLaser,
            Target = 1,
            Amount = 0,
            Achieved = false
        }
	#endregion
    };
}
