using UnityEngine;

public class UserInfomations
    {
        private const string HEART_COUNT = "HeartCount";
        // 하트 수
        public static int HeartCount
        {
            get
            {
                return PlayerPrefs.GetInt(HEART_COUNT, 5);
            }
            set
            {
                PlayerPrefs.SetInt(HEART_COUNT,value);
            }
        }
    }
