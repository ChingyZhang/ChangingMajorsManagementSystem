using System;

namespace ValueOffice.Word
{
    public interface IWord
    {
        Boolean Create(String fileName);

        Boolean Create(String fileName, String password);

        void Write(String fileName, String sentence);

        void Write(String fileName, String sentence, String password);
    }
}
