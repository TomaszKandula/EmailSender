﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmailSenderService.Backend.Shared.Resources {
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
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("EmailSenderService.Backend.Shared.Resources.ErrorCodes", typeof(ErrorCodes).Assembly);
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
        
        public static string SMTP_CLIENT_ERROR {
            get {
                return ResourceManager.GetString("SMTP_CLIENT_ERROR", resourceCulture);
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
    }
}
