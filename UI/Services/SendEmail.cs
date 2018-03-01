using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace UI.Services
{
    public class EmailSend
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="eMail">接收方邮箱</param>
        /// <param name="mailName">发件人名称</param>
        /// <param name="mailTitle">邮件名称</param>
        /// <returns></returns>
        public static Tuple<string, bool> SendEmail(string eMail, string mailName, string mailTitle)
        {
            MailHelper mail = new MailHelper();
            mail.MailServer = "smtp.qq.com";
            mail.MailboxName = "444503829@qq.com";
            mail.MailboxPassword = "qrluwfrgtdnzbjbd";//开启QQ邮箱POP3/SMTP服务时给的授权码
            //操作打开QQ邮箱->在账号下方点击"设置"->账户->POP3/IMAP/SMTP/Exchange/CardDAV/CalDAV服务
            //obxxsfowztbideee为2872845261@qq的授权码
            mail.MailName = mailName;
            string code;
            VerifyCode codeHelper = new VerifyCode();
            codeHelper.GetVerifyCode(out code);
            if (code == "")
                return new Tuple<string, bool>("", false);
            if (mail.Send(eMail, mailTitle, code))
                return new Tuple<string, bool>(code, true);
            return new Tuple<string, bool>("", false);
        }
    }
}