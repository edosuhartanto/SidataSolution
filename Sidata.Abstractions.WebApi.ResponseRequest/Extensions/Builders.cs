using Sidata.Abstractions.WebApi.ResponseRequest.Models;

namespace Sidata.Abstractions.WebApi.ResponseRequest.Extensions
{
    public static class Builders
    {
        #region REQUEST Data Builders

        #region request content detail data builder
        /// <summary>
        /// generic request parameters builder specific 
        /// for request a block of Detail with Id_Header as common filter.
        /// set pagenumber and pagesize into non-zero 
        /// for requesting in paging mode
        /// </summary>
        public static RequestDetailData<TParam> 
                        BuildRequestDetailData<TParam>(
                            long id_header, List<TParam> parameters)
        {
            return new()
            {
                Data = parameters,
                Id_Header = id_header
            };
        }

        /// <summary>
        /// special build the request for detail entity 
        /// (using id_header as common filter)
        /// with single parameter
        /// without paging mode.
        /// </summary>
        public static RequestDetailData<TParam> 
                        BuildRequestDetailData<TParam>(
                            long id_header, TParam parameter)
        {
            return BuildRequestDetailData(id_header, [parameter]);
        }
        #endregion

        #region request content data builder
        /// <summary>
        /// generic request parameters builder, 
        /// with list of parameters.
        /// Set pagenumber and pagesize into non-zero 
        /// for requesting in paging mode.
        /// </summary>
        public static RequestData<TParam> 
                        BuildRequestData<TParam>(
                            List<TParam> parameters)
        {
            return new()
            {
                Data = parameters
            };
        }

        /// <summary>
        /// special request parameters builder, 
        /// with single parameter
        /// without paging mode
        /// </summary>
        public static RequestData<TParam> 
                        BuildRequestData<TParam>(
                            TParam parameter)
        {
            return BuildRequestData([parameter]);
        }
        #endregion

        #endregion

        #region RESPONSE Data Builders

        #region response content data builder
        /// <summary>
        /// build a responsecontent data for response from an endpoint
        /// with/without paging mode.
        /// TotalSize = total all data should return by endpoint
        /// if without paging mode, pagenumber and pagesize should set to 0
        /// if with paging mode,
        /// PageNumber = page number where the block of data belong to
        /// PageSize = total data in one page
        /// </summary>
        public static ResponseContentData<TContent> 
                        BuildResponseContentData<TContent>(
                            List<TContent> datacontents, 
                            long totalsize, 
                            int pagenumber, 
                            int pagesize)
        {
            return new()
            {
                TotalSize = totalsize,
                PageNumber = pagenumber,
                PageSize = pagesize,
                Data = datacontents
            };
        }

        /// <summary>
        /// build a special data responsecontent with list of return values
        /// without paging
        /// </summary>
        public static ResponseContentData<TContent> 
                        BuildResponseContentData<TContent>(
                            List<TContent> datacontents)
        {
            return BuildResponseContentData(datacontents, 
                                            datacontents.Count, 
                                            0, 0);
        }

        /// <summary>
        /// build a special data responsecontent with a single return values
        /// without paging
        /// </summary>
        public static ResponseContentData<TContent> 
                        BuildResponseContentData<TContent>(TContent datacontent)
        {
            return BuildResponseContentData([datacontent]);
        }
        #endregion

        #region Ok Response Data Builder
        /// <summary>
        /// build a generic response ok,
        /// warning message can be send too (default is no message)
        /// </summary>
        public static ResponseData<TContent> 
                        BuildOkResponseData<TContent>(
                            this ResponseContentData<TContent> responsecontent,
                            string? responsewarningmessage = null)
        {
            return new()
            {
                Content = responsecontent,
                ReturnMessage = responsewarningmessage ?? string.Empty
            };
        }

        // response ok with List of parameters TParam with paging
        public static ResponseData<TContent> 
                        BuildOkResponseData<TContent>(
                            this List<TContent> datacontents, 
                            long totalsize, 
                            int pagenumber, 
                            int pagesize,
                            string? responsewarningmessage = null)
        {
            return BuildResponseContentData(datacontents, totalsize, 
                                             pagenumber, pagesize)
                    .BuildOkResponseData(responsewarningmessage);
        }

        /// <summary>
        /// special response ok with List of datacontents
        /// without paging mode
        /// </summary>
        public static ResponseData<TContent> 
                        BuildOkResponseData<TContent>(
                            this List<TContent> datacontents,
                            string? responsewarningmessage = null)
        {
            return BuildResponseContentData(datacontents) 
                        .BuildOkResponseData(responsewarningmessage);
        }

        /// <summary>
        /// special response ok with a single datacontent
        /// without paging mode
        /// </summary>
        public static ResponseData<TContent> 
                        BuildOkResponseData<TContent>(
                            this TContent datacontent,
                            string? responsewarningmessage = null)
        {
            return BuildResponseContentData(datacontent) 
                        .BuildOkResponseData(responsewarningmessage);
        }
        #endregion

        #region Error response builder
        /// <summary>
        /// generic error response builder. 
        /// Error Response always return in string type.
        /// </summary>
        public static ResponseData<string> 
                        BuildErrorResponseData(
                            long responseid, 
                            string responsemessage, 
                            List<string> extendederrormessages)
        {
            return new()
            {
                Content = BuildResponseContentData(extendederrormessages),
                ReturnId = responseid,
                ReturnMessage = responsemessage
            };
        }

        /// <summary>
        /// specific error response builder that gets its error messages
        /// from exception object.
        /// format exception message should be "msg:msg:...:lastmsg" 
        /// separated by colon and lastmsg will become response message
        /// content array will build as follow
        /// "Msg='complete exception.message'",
        /// "(opsional)Source='exception.source'",
        /// "(opsional)StackTrace='exception.stacktrace'",
        /// "(opsional)InnerException='exception.innerexception'".
        /// </summary>
        public static ResponseData<string>
                        BuildErrorResponseData(
                            this Exception exception,
                            long responseid)
        {
            var s = exception.Message.Split(':');
            string responsemessage = s.LastOrDefault() ?? "unknown error";
            var a = new List<string>([$"Msg={exception.Message}"]);
            if (exception.Source != null) 
            { 
                a.Add($"Source={exception.Source}"); 
            }
            if (exception.StackTrace != null) 
            { 
                a.Add($"StackTrace={exception.StackTrace}"); 
            }
            var i = exception.InnerException?.ToString() ?? string.Empty;
            if (i.Length > 0)
            {
                a.Add($"InnerException={i}");
            }            
            return BuildErrorResponseData(
                        responseid,
                        responsemessage,
                        a);
        }
        #endregion

        #endregion

        public static void ThrowIfContentNull<TData>(
                        this RequestData<TData> request)
        {
            if (request.Data.Count == 0)
                throw new ArgumentException(
                    "Argumen dalam request data tidak boleh kosong",
                    nameof(request));
        }

        public static void ThrowIfContentNullOrMultipleItem<TData>(
                                this RequestData<TData> request)
        {
            request.ThrowIfContentNull();
            if (request.Data.Count > 1)
                throw new ArgumentException(
                    "Jumlah data yang dikirim harus tepat satu",
                    nameof(request));
        }


    }
}
