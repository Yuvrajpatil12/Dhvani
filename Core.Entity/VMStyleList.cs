using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Entity
{
    public class VMStyleList
    {
        #region Declarations

        private bool _boolObjectChanged;
        private int _intID;
        private int _intVoiceMasterID;
        private string _strStyleName;
        private byte _bytStatusId;
        private DateTime _datCreatedDate;
        private DateTime _datUpdateDate;


        #endregion Declarations

        #region Properties

        public bool ObjectChanged
        {
            get { return this._boolObjectChanged; }
            set { this._boolObjectChanged = value; }
        }

        public int ID
        {
            get { return this._intID; }
            set { this._intID = value; }
        }

        public int VoiceMasterID
        {
            get { return this._intVoiceMasterID; }
            set { this._intVoiceMasterID = value; }
        }

        public string StyleName
        {
            get { return this._strStyleName; }
            set { this._strStyleName = value; }
        }

        public byte StatusId
        {
            get { return this._bytStatusId; }
            set { this._bytStatusId = value; }
        }

        public DateTime CreatedDate
        {
            get { return this._datCreatedDate; }
            set { this._datCreatedDate = value; }
        }

        public DateTime UpdateDate
        {
            get { return this._datUpdateDate; }
            set { this._datUpdateDate = value; }
        }



        #endregion Properties
    }
}
