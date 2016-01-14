﻿
// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using System.Net;

namespace Microsoft.Azure.Commands.WebApps.Cmdlets.WebApps
{
    /// <summary>
    /// this commandlet will let you delete an Azure web app
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRMWebApp"), OutputType(typeof(HttpStatusCode))]
    [CliCommandAlias("appservice rm")]
    public class RemoveAzureWebAppCmdlet : WebAppBaseCmdlet
    {

       //always delete the slots, 
        private bool deleteSlotsByDefault = true;

        // leave behind the empty webhosting plan 
        private bool deleteEmptyServerFarmByDefault = false;

        //always delete the metrics
        private bool deleteMetricsByDefault = true;

        [Parameter(Mandatory = false, HelpMessage = "Do not ask for confirmation.")]
        [Alias("f")]
        public SwitchParameter Force { get; set; }
            
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            ConfirmAction(
                Force.IsPresent,
                string.Format(WebSites.Properties.Resources.RemoveWebsiteWarning, Name),
                WebSites.Properties.Resources.RemoveWebsiteMessage,
                Name,
                () =>
                {
                    WebsitesClient.RemoveWebApp(ResourceGroupName, Name, null, deleteEmptyServerFarmByDefault, deleteMetricsByDefault, deleteSlotsByDefault);
                });

        }
    }
}



