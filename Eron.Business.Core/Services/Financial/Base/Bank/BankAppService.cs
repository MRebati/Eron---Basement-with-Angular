using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.MellatBankService;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Exceptions;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.TokenGenerator;

namespace Eron.Business.Core.Services.Financial.Base.Bank
{
    public class BankAppService : ManagementSystemService, IBankAppService
    {
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static readonly string MellatPgwSite = ConfigurationManager.AppSettings["Mellat.PgwSite"];
        public static readonly string MellatTerminalId = ConfigurationManager.AppSettings["Mellat.TerminalId"];
        public static readonly string MellatUserName = ConfigurationManager.AppSettings["Mellat.UserName"];
        public static readonly string MellatUserPassword = ConfigurationManager.AppSettings["Mellat.UserPassword"];

        public BankAppService(IManagementUnitOfWork unitOfWork, TenantType tenantType = TenantType.WebService) : base(unitOfWork, tenantType)
        {
        }

        public async Task<string> CreateReference(BankNameType bankName, string userId, long invoiceId)
        {
            string referenceId = "";

            var invoiceNumber = (await UnitOfWork.InvoiceRepository.GetByIdAsync(invoiceId)).InvoiceNumber;
            var amount = await UnitOfWork.InvoiceRepository.GetInvoiceAmount(invoiceId);

            var transactionNumber = TokenGenerator.Generate(TokenType.Transaction);

            switch (bankName)
            {
                case BankNameType.Mellat:
                    try
                    {
                        string result = "";
                        BypassCertificateError();
                        MellatBankService.PaymentGatewayClient bp = new MellatBankService.PaymentGatewayClient();
                        result = bp.bpPayRequest(
                            Int64.Parse(MellatTerminalId),
                            MellatUserName,
                            MellatUserPassword,
                            invoiceId,
                            amount,
                            SetDefaultDate(),
                            SetDefaultTime(),
                            $"Invoice #{invoiceNumber}",
                            CallBackUrl,
                            0);
                        string[] res = result.Split(',');
                        if (res[0] == "0")
                        {
                            var financialTransaction = new FinanceTransaction(invoiceId, transactionNumber, userId,res[1]);
                            UnitOfWork.FinanceTransactionRepository.Create(financialTransaction);
                            await UnitOfWork.SaveAsync();

                            return res[1];
                        }
                        else
                        {
                            return BankResultException(res[0], BankNameType.Mellat);
                        }
                    }
                    catch
                    {
                        return BankResultException("", BankNameType.Mellat);
                    }
                //case BankNameType.Keshavarzi:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bankName), bankName, null);
            }
            return referenceId;
        }

        #region Helpers


        private void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }

        private string SetDefaultDate()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');

        }
        private string SetDefaultTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }

        private string BankResultException(string resCode, BankNameType bankName)
        {
            string result;

            switch (bankName)
            {
                case BankNameType.Mellat:
                    result = GetMellatBankResCodeResponse(resCode);
                    return result;
                //case BankNameType.Keshavarzi:
                //    result = GetKeshavarziBankResCodeResponse(resCode);
                //    return result;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bankName), bankName, null);
            }
        }

        private string GetKeshavarziBankResCodeResponse(string resCode)
        {
            throw new NotImplementedException();
        }

        private string GetMellatBankResCodeResponse(string resCode)
        {
            var finalResponse = "";
            var result = Int32.Parse(resCode);
            switch (result)
            {
                case 0:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 11:
                    finalResponse = "شماره كارت نامعتبر است";
                    break;
                case 12:
                    finalResponse = "موجودي كافي نيست";
                    break;
                case 13:
                    finalResponse = "رمز نادرست است";
                    break;
                case 14:
                    finalResponse = "تعداد دفعات وارد كردن رمز بيش از حد مجاز است";
                    break;
                case 15:
                    finalResponse = "كارت نامعتبر است";
                    break;
                case 16:
                    finalResponse = "دفعات برداشت وجه بيش از حد مجاز است";
                    break;
                case 17:
                    finalResponse = "كاربر از انجام تراكنش منصرف شده است";
                    break;
                case 18:
                    finalResponse = "تاريخ انقضاي كارت گذشته است";
                    break;
                case 19:
                    finalResponse = "مبلغ برداشت وجه بيش از حد مجاز است";
                    break;
                case 111:
                    finalResponse = "صادر كننده كارت نامعتبر است";
                    break;
                case 112:
                    finalResponse = "خطاي سوييچ صادر كننده كارت";
                    break;
                case 113:
                    finalResponse = "پاسخي از صادر كننده كارت دريافت نشد";
                    break;
                case 114:
                    finalResponse = "دارنده كارت مجاز به انجام اين تراكنش نيست";
                    break;
                case 21:
                    finalResponse = "پذيرنده نامعتبر است";
                    break;
                case 23:
                    finalResponse = "خطاي امنيتي رخ داده است";
                    break;
                case 24:
                    finalResponse = "اطلاعات كاربري پذيرنده نامعتبر است";
                    break;
                case 25:
                    finalResponse = "مبلغ نامعتبر است";
                    break;
                case 31:
                    finalResponse = "پاسخ نامعتبر است";
                    break;
                case 32:
                    finalResponse = "فرمت اطلاعات وارد شده صحيح نمي باشد";
                    break;
                case 33:
                    finalResponse = "حساب نامعتبر است";
                    break;
                case 34:
                    finalResponse = "خطاي سيستمي";
                    break;
                case 35:
                    finalResponse = "تاريخ نامعتبر است";
                    break;
                case 41:
                    finalResponse = "شماره درخواست تكراري است";
                    break;
                case 42:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 43:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 44:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 45:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 46:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 47:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 48:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 49:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 412:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 413:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 414:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 415:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 416:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 417:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 418:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 419:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 421:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 51:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 54:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 55:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                case 61:
                    finalResponse = "تراكنش با موفقيت انجام شد";
                    break;
                default:
                    finalResponse = "مشکلی نامشخص از سمت بانک رخ داده است";
                    break;
            }
            return finalResponse;
        }

        #endregion
    }
}
