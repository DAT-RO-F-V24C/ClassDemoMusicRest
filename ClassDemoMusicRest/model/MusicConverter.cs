using ClassDemoMusicLib.model;

namespace ClassDemoMusicRest.model
{
    public static class MusicConverter
    {
        public static Music DTO2Music(MusicDTO dto)
        {
            Music music = new Music();
            music.Id = dto.Id;
            music.Title = dto.Title; // kaster evt arg.exception
            music.Year = dto.Year;// kaster evt arg.exception

            return music;
        }
    }
}
