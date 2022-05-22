using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QB.Wallpaper
{
    [DefaultExecutionOrder(0)]
    public class WallpaperPool : MonoBehaviour
    {
        private static WallpaperPool _instance;

        public static WallpaperPool Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance =
                        new GameObject(nameof(WallpaperPool), typeof(WallpaperPool)).GetComponent<WallpaperPool>();
                }

                return _instance;
            }
        }


        [SerializeField] private Wallpaper wallpaperTemplate;


        private void Awake()
        {
            _instance = this;
        }

        //[ShowInInspector]
        private Dictionary<WallpaperUser, Wallpaper> WallpaperUsers { get; } = new Dictionary<WallpaperUser, Wallpaper>();

        // [ShowInInspector]
        private List<Wallpaper> UnUsedWallpapers { get; } = new List<Wallpaper>();


        public void Use(WallpaperUser user)
        {
            if (WallpaperUsers.ContainsKey(user)) return;

            var wallpaper = UnUsedWallpapers.Any() ? UnUsedWallpapers.First() : Instantiate(wallpaperTemplate);
            
            if (UnUsedWallpapers.Contains(wallpaper)) UnUsedWallpapers.Remove(wallpaper);
            UnUsedWallpapers.ForEach(unUsedWallpaper => unUsedWallpaper.transform.SetParent(transform));
            
            wallpaper.SetParent(user.RectTransform);
            wallpaper.SetSkin(user.preset);
            
            WallpaperUsers.Add(user, wallpaper);
        }

        public void Recall(WallpaperUser user)
        {
            if (WallpaperUsers.ContainsKey(user))
            {
                var wallpaper = WallpaperUsers[user];
                UnUsedWallpapers.Add(wallpaper);

                WallpaperUsers.Remove(user);
            }
        }
    }
}