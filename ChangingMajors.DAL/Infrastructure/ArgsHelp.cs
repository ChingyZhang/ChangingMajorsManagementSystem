using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Infrastructure
{
    public class ArgsHelp
    {
        private String msg;
        public String Msg
        {
            set
            {
                this.msg = value;
                this.flag = false;
            }
            get
            {
                return msg;
            }
        } //消息

        private Boolean flag;
        public Boolean Flag
        {
            set
            {
                this.flag = value;
            }
            get
            {
                return flag;
            }
        }

        private String returnValue;
        public String ReturnValue
        {
            set
            {
                this.returnValue = value;
            }
            get
            {
                return returnValue;
            }
        }

        public ArgsHelp()
        {
            flag = true;
            msg = "";
            returnValue = "";
        }

        public ArgsHelp(Boolean flag)
        {
            this.flag = flag;
        }

        public ArgsHelp(String msg)
        {
            this.msg = msg;
            this.flag = false;
        }

        public ArgsHelp(String msg, Boolean flag)
        {
            this.msg = msg;
            this.flag = flag;
        }

        public ArgsHelp(String msg, Boolean flag, String returnValue)
        {
            this.msg = msg;
            this.flag = flag;
            this.returnValue = returnValue;
        }

        //public ArgsHelp(String msg, Boolean flag, String returnValue)
        //{
        //    this.msg = msg;
        //    this.flag = flag;
        //    this.returnValue = returnValue;
        //}

        //public ArgsHelp(String returnValue)
        //{
        //    this.returnValue = returnValue;
        //}

    }
}
