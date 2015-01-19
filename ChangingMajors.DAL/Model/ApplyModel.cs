using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Model
{
    public class ApplyModel
    {

        private int _entryID;
        private String _userID;
        private String _majorName;
        private String _sysMajorName;
        private int _startID;
        private int _entryState;
        private int _entryFlag;



        public int EntryID
        {
            get { return _entryID; }
            set { _entryID = value; }
        }
        public String UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        public String MajorName
        {
            get { return _majorName; }
            set { _majorName = value; }
        }
        public String SysMajorName
        {
            get { return _sysMajorName; }
            set { _sysMajorName = value; }
        }
        public int StartID
        {
            get { return _startID; }
            set { _startID = value; }
        }
        public int EntryState
        {
            get { return _entryState; }
            set { _entryState = value; }
        }
        public int EntryFlag
        {
            get { return _entryFlag; }
            set { _entryFlag = value; }
        }

    }
}
