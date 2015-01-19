/*
  >>>----- Copyright (c) 2012 zformular -----> 
 |                                            |
 |              Author: zformular             |
 |          E-mail: zformular@163.com         |
 |               Date: 05.16.2013             |
 |                                            |
 ╰==========================================╯
 
 */

using System;

namespace ValueOffice.Word
{
    public class ValueWord : IWord
    {
        #region IWord 成员

        public bool Create(string fileName)
        {
            return this.Create(fileName, null);
        }

        public bool Create(string fileName, string password)
        {
            if (System.IO.File.Exists(fileName))
                return false;

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = null;
            Object missing = System.Reflection.Missing.Value;
            try
            {
                Object name = fileName;
                Object pwd = missing;
                if (!String.IsNullOrEmpty(password))
                    pwd = password;
                document = word.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                document.SaveAs(ref name, ref missing, ref missing, ref pwd, ref missing, ref pwd, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                document.Close(ref missing, ref missing, ref missing);
                word.Quit(ref missing, ref missing, ref missing);
            }
        }

        public void Write(string fileName, string sentence)
        {
            this.Write(fileName, sentence, null);
        }

        public void Write(string fileName, string sentence, string password)
        {
            if (System.IO.File.Exists(fileName)) return;

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document document = null;
            Object missing = System.Reflection.Missing.Value;
            try
            {
                Object name = fileName;
                Object pwd = missing;
                if (!String.IsNullOrEmpty(password))
                    pwd = password;
                document = word.Documents.Open(ref name, ref missing, ref missing, ref missing, ref pwd, ref pwd, ref missing,
                    ref pwd, ref pwd, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                document.Save();
            }
            finally
            {
                document.Close(ref missing, ref missing, ref missing);
                word.Quit(ref missing, ref missing, ref missing);
            }
        }

        #endregion
    }
}
