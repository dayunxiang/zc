using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL {

    public class LineRange {
        #region IncludeBeginPoint
        /// <summary>
        /// 
        /// </summary>
        public bool IncludeBeginPoint {
            get { return _includeBeginPoint; }
            set { _includeBeginPoint = value; }
        } private bool _includeBeginPoint = false;
        #endregion //IncludeBeginPoint

        #region IncludeEndPoint
        /// <summary>
        /// 
        /// </summary>
        public bool IncludeEndPoint {
            get { return _includeEndPoint; }
            set { _includeEndPoint = value; }
        } private bool _includeEndPoint = false;
        #endregion //IncludeEndPoint

        #region LineRange
        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public LineRange(decimal begin, decimal end) {
            if (begin > end) {
                var t = begin;
                begin = end;
                end = t;
            }

            this._begin = begin;
            this._end = end;
        }
        #endregion //decimalRange

        #region Begin
        /// <summary>
        /// 
        /// </summary>
        public decimal Begin {
            get {
                return _begin;
            }
        } private decimal _begin;
        #endregion //Begin

        #region End
        /// <summary>
        /// 
        /// </summary>
        public decimal End {
            get {
                return _end;
            }
        } private decimal _end;
        #endregion //End

        #region IsEarly
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool IsEarly(decimal val) {
            if (this.IncludeBeginPoint) {
                return val < this._begin;
            } else {
                return val <= this._begin;
            }
        }
        #endregion //IsEarly

        #region IsLater
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool IsLater(decimal val) {
            if (this.IncludeEndPoint) {
                return val > this._end;
            } else {
                return val >= this._end;
            }
        }
        #endregion //IsLater

        #region IsInRange
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool IsInRange(decimal val) {
            bool r = false;
            if (IncludeBeginPoint) {
                r = val >= this._begin;
            } else {
                r = val > this._begin;
            }

            if (r) {
                if (IncludeEndPoint) {
                    r = val <= this._end;
                } else {
                    r = val < this._end;
                }
            }
            return r;
        }
        #endregion //IsInRange


        #region DiscernRelation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public LineRangeRelation DiscernRelation(LineRange other) {
            if (other == null) {
                throw new ArgumentNullException("other");
            }

            bool b = this.IsInRange(other.Begin);
            bool e = this.IsInRange(other.End);

            LineRangeRelation[,] table = new LineRangeRelation[2, 2] {
                // e = false                           e = true 
                { LineRangeRelation.Disconnection, LineRangeRelation.CrossAtBegin }, // b = false
                    { LineRangeRelation.CrossAtEnd,    LineRangeRelation.Include },      // b = true
            };

            LineRangeRelation r = table[b ? 1 : 0, e ? 1 : 0];

            if (r == LineRangeRelation.Disconnection) {
                if (IsEarly(other.Begin) && IsLater(other.End)) {
                    r = LineRangeRelation.BeIncluded;
                }
            }

            return r;
        }
        #endregion //DiscernRelation

        #region ToString
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            string s = string.Format("{0}{1} ~ {2}{3}",
                    _includeBeginPoint ? '[' : '(', this._begin,
                    this._end, _includeEndPoint ? ']' : ')'
                    );
            return s;
        }
        #endregion //ToString


        #region Tag
        /// <summary>
        /// 
        /// </summary>
        public object Tag {
            get {
                return _tag;
            }
            set {
                _tag = value;
            }
        } private object _tag;
        #endregion //Tag
    }
}
