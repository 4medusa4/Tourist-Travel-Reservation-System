using System.ComponentModel;
using System.Net.Mail;
using System.Windows;

namespace LoginPage.Utilities
{
    internal class FileSender
    {
        private AttachmentCollection attachments;
        private MailAddressCollection to;
        private MailAddressCollection cc;
        private string subject;
        private string body;
        private static bool mailSent = false;
        private static MailMessage mail;
        private SmtpClient client;

        public string Subject { get => subject; set => subject = value; }
        public string Body { get => body; set => body = value; }
        public static bool MailSent { get => mailSent; private set => mailSent = value; }

        public FileSender()
        {
            this.attachments = new MailMessage().Attachments;
            attachments.Clear();
            this.to = new MailAddressCollection();
            this.cc = new MailAddressCollection();
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            string token = (string)e.UserState;
            if (e.Cancelled)
            {
                MessageBox.Show(token + " Send canceled.",
                    "Message wizard", MessageBoxButton.OK,
                    MessageBoxImage.Information, MessageBoxResult.OK,
                    MessageBoxOptions.None);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(token + "\n" + e.Error.ToString(),
                    "Message wizard", MessageBoxButton.OK,
                    MessageBoxImage.Error, MessageBoxResult.OK,
                    MessageBoxOptions.ServiceNotification);
            }
            else
            {
                MailSent = true;
                MessageBox.Show("Message sent.", "Message wizard",
                    MessageBoxButton.OK, MessageBoxImage.Information,
                    MessageBoxResult.OK, MessageBoxOptions.None);
            }
            // Cleans up
            mail.Dispose();
            ((SmtpClient)sender).Dispose();
        }
        private void setMessage()
        {
            mail = new MailMessage();
            client = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("ceylontravellers.srilanka@gmail.com");
            mail.Subject = Subject;
            mail.Body = Body;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            // setting up recievers
            foreach (MailAddress r in to)
                mail.To.Add(r);
            // setting up cc
            if (cc != null)
            {
                foreach (MailAddress r in cc)
                    mail.CC.Add(r);
            }
            // setting up attachments
            if (attachments != null)
            {
                foreach (Attachment att in attachments)
                    mail.Attachments.Add(att);
            }

            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("ceylontravellers.srilanka@gmail.com", "ceylon@srilanka");
            client.EnableSsl = true;

            // timeout in ms
            client.Timeout = 100;
        }

        public void sendAsync()
        {
            if (to.Count > 0)
            {
                setMessage();
                string userState = "Promotion message.";
                try
                {
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                    // sends msg asynchronously
                    client.SendAsync(mail, userState);
                    MessageBoxResult result = MessageBox.Show(
                        "Send mail?",
                        "Message wizard",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Information,
                        MessageBoxResult.No);

                    if (result == MessageBoxResult.Cancel && MailSent == false)
                    {
                        client.SendAsyncCancel();
                    }
                }
                catch (SmtpException e)
                {
                    MessageBox.Show("Message timeout exeeded.\n" + e.Message.ToString(),
                        "Message wizard", MessageBoxButton.OK, MessageBoxImage.Error,
                        MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                catch (System.Exception e)
                {
                    MessageBox.Show("Error: " + e.Message.ToString(),
                        "Message wizard", MessageBoxButton.OK, MessageBoxImage.Error,
                        MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            else
            {
                MessageBox.Show("Recepient address not given.",
                        "Message wizard", MessageBoxButton.OK, MessageBoxImage.Error,
                        MessageBoxResult.OK);
            }

        }

        public bool sendSync()
        {
            bool flag = false;
            if (to.Count > 0)
            {
                setMessage();
                try
                {
                    // sends message synchronously
                    client.Send(mail);
                    flag = true;
                }
                catch (SmtpFailedRecipientsException e)
                {
                    for (int i = 0; i < e.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = e.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            MessageBox.Show("Delivery failed - retrying in 5 seconds.",
                        "Message wizard", MessageBoxButton.OK, MessageBoxImage.Warning,
                        MessageBoxResult.OK);
                            System.Threading.Thread.Sleep(5000);
                            client.Send(mail);
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            MessageBox.Show("Failed to deliver message to " + e.InnerExceptions[i].FailedRecipient,
                        "Message wizard", MessageBoxButton.OK, MessageBoxImage.Error,
                        MessageBoxResult.OK);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    flag = true;
                    MessageBox.Show("Error: " + e.Message.ToString(),
                        "Message wizard", MessageBoxButton.OK, MessageBoxImage.Error,
                        MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
            }
            mail.Dispose();
            client.Dispose();
            return flag;
        }

        public void addAttachment(string file_path)
        {
            this.attachments.Add(new Attachment(file_path));
        }

        public void addReciever(string reciever_address)
        {
            this.to.Add(new MailAddress(reciever_address));
        }

        public void addCc(string cc_address)
        {
            this.cc.Add(new MailAddress(cc_address));
        }

        public void removeAttachment(string file_path)
        {
            if (this.attachments.Contains(new Attachment(file_path)))
                this.attachments.RemoveAt(attachments.IndexOf(new Attachment(file_path)));
        }

        public void clearAttachments()
        {
            this.attachments.Clear();
        }

        public void removeRecepient(string reciever_address)
        {
            if (this.to.Contains(new MailAddress(reciever_address)))
                this.to.RemoveAt(to.IndexOf(new MailAddress(reciever_address)));
        }

        public void clearRecepients()
        {
            this.to.Clear();
        }

        public void removeCc(string cc_address)
        {
            if (this.cc.Contains(new MailAddress(cc_address)))
                this.cc.RemoveAt(cc.IndexOf(new MailAddress(cc_address)));
        }

        public void clearCc()
        {
            this.cc.Clear();
        }
    }
}
