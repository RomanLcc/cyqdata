using System;
using System.Collections.Generic;
using System.Text;
using CYQ.Data.Orm;

namespace CYQ.Data
{
    /// <summary>
    /// A class for you to Write log to database
    /// <para>��־��¼�����ݿ⣨��Ҫ����LogConn���Ӻ���Ч��</para>
    /// </summary>
    public partial class SysLogs : Orm.SimpleOrmBase
    {

        public SysLogs()
        {
            base.SetInit(this, AppConfig.Log.LogTableName, AppConfig.Log.LogConn);
        }

        private int _ID;
        /// <summary>
        /// ��ʶ����
        /// </summary>
        [Key(true, true, false)]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private string _LogType;
        /// <summary>
        /// ��־����
        /// </summary>
        [Length(50)]
        public string LogType
        {
            get { return _LogType ?? ""; }
            set { _LogType = value; }
        }
        private string _Host;
        /// <summary>
        /// �����������ַ
        /// </summary>
        [Length(100)]
        public string Host
        {
            get
            {
                if (string.IsNullOrEmpty(_Host))
                {
                    _Host = LocalEnvironment.HostIP;
                }
                return _Host;
            }
            set
            {
                _Host = value;
            }
        }

        private string _PageUrl;
        /// <summary>
        /// ����ĵ�ַ
        /// </summary>
        [Length(500)]
        public string PageUrl
        {
            get
            {
                return _PageUrl;
            }
            set
            {
                _PageUrl = value;
            }
        }

        private string _RefererUrl;
        /// <summary>
        /// ����ĵ�ַ
        /// </summary>
        [Length(500)]
        public string RefererUrl
        {
            get
            {
                return _RefererUrl;
            }
            set
            {
                _RefererUrl = value;
            }
        }


        private string _Message;
        /// <summary>
        /// ��־����
        /// </summary>
        [Length(2000)]
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }

        private DateTime _CreateTime;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                if (_CreateTime == DateTime.MinValue)
                {
                    _CreateTime = DateTime.Now;
                }
                return _CreateTime;
            }
            set
            {
                _CreateTime = value;
            }
        }
    }

    public partial class SysLogs
    {
        /// <summary>
        /// �Ƿ�д���ı���
        /// </summary>
        internal bool IsWriteToTxt = false;
        /// <summary>
        /// ��ȡ�ı���ʽ����־����
        /// </summary>
        /// <returns></returns>
        internal string GetFormatterText()
        {
            string title = string.Format("V{0} Record On : {1} : {2}",
                AppConfig.Version, DateTime.Now.ToString(), PageUrl??"");// + Log.Url;
            if (!string.IsNullOrEmpty(RefererUrl))
            {
                title += AppConst.NewLine + AppConst.NewLine + "Referer : " + RefererUrl;
            }
            string body = title + AppConst.NewLine + AppConst.NewLine + Message + AppConst.NewLine + AppConst.NewLine;
            body += "---------------------------------------" + AppConst.NewLine + AppConst.NewLine;
            return body;
        }
    }
}