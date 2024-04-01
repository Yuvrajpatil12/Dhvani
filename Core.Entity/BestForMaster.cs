using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Entity
{
    public class BestForMaster
    {
        #region Declarations

        private bool _boolObjectChanged;
        private Int64 _intID;
        private string _strBestForName;
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

        public Int64 ID
        {
            get { return this._intID; }
            set { this._intID = value; }
        }

        public string BestForName
        {
            get { return this._strBestForName; }
            set { this._strBestForName = value; }
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
