﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmailSender.Backend.Shared.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorCodes {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorCodes() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("EmailSender.Backend.Shared.Resources.ErrorCodes", typeof(ErrorCodes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string ERROR_UNEXPECTED {
            get {
                return ResourceManager.GetString("ERROR_UNEXPECTED", resourceCulture);
            }
        }
        
        public static string SMTP_NOT_AUTHENTICATED {
            get {
                return ResourceManager.GetString("SMTP_NOT_AUTHENTICATED", resourceCulture);
            }
        }
        
        public static string SMTP_NOT_CONNECTED {
            get {
                return ResourceManager.GetString("SMTP_NOT_CONNECTED", resourceCulture);
            }
        }
        
        public static string INVALID_PRIVATE_KEY {
            get {
                return ResourceManager.GetString("INVALID_PRIVATE_KEY", resourceCulture);
            }
        }
        
        public static string INVALID_ASSOCIATED_USER {
            get {
                return ResourceManager.GetString("INVALID_ASSOCIATED_USER", resourceCulture);
            }
        }
        
        public static string INVALID_ASSOCIATED_EMAIL {
            get {
                return ResourceManager.GetString("INVALID_ASSOCIATED_EMAIL", resourceCulture);
            }
        }
        
        public static string MISSING_SERVER_DATA {
            get {
                return ResourceManager.GetString("MISSING_SERVER_DATA", resourceCulture);
            }
        }
        
        public static string MISSING_EMAIL_DATA {
            get {
                return ResourceManager.GetString("MISSING_EMAIL_DATA", resourceCulture);
            }
        }
        
        public static string ACCESS_FORBIDDEN {
            get {
                return ResourceManager.GetString("ACCESS_FORBIDDEN", resourceCulture);
            }
        }
        
        public static string FILE_NOT_FOUND {
            get {
                return ResourceManager.GetString("FILE_NOT_FOUND", resourceCulture);
            }
        }
        
        public static string USER_EMAIL_ALREADY_EXIST {
            get {
                return ResourceManager.GetString("USER_EMAIL_ALREADY_EXIST", resourceCulture);
            }
        }
        
        public static string USER_DOES_NOT_EXIST {
            get {
                return ResourceManager.GetString("USER_DOES_NOT_EXIST", resourceCulture);
            }
        }
        
        public static string INVALID_EMAIL_ID {
            get {
                return ResourceManager.GetString("INVALID_EMAIL_ID", resourceCulture);
            }
        }
        
        public static string INVALID_ID {
            get {
                return ResourceManager.GetString("INVALID_ID", resourceCulture);
            }
        }
        
        public static string INSUFFICIENT_PRIVILEGES {
            get {
                return ResourceManager.GetString("INSUFFICIENT_PRIVILEGES", resourceCulture);
            }
        }
    }
}
