# GAB19
Content for the 2019 Global Azure Bootcamp


# Leveraging Log Analytics in Custom Applications
Log Analytics demo for Global Azure Bootcamp 2019

## Create a new Log Analytics Workspace

1. If you haven't already, sign into the [Azure Portal](https://portal.azure.com).
2. Click on **Create a resource**. 
3. Search for **Log Analytics**.
4. Select the appropriate resource for Log Analytics and the click **Create**.

![Create A Workspace][Create1]

[Create1]: https://github.com/achance/GAB19/blob/master/Screenshots/CreateWorkspace.PNG?raw=true "Create a Workspace"

5. Fill out the required info. Feel free to create a new resource group, or use an existing one.

![Create a Workspace 2][Create2]

[Create2]: https://github.com/achance/GAB19/blob/master/Screenshots/CreateWorkspace2.PNG?raw=true "Create a Workspace 2"

6. Wait a few moments for the creation to complete. You can track the status through the notifications. 
7. Once the setup is complete, select your newly created workspace.

![Create a Workspace 3][Create3]

[Create3]: https://github.com/achance/GAB19/blob/master/Screenshots/CreateWorkspace3.PNG?raw=true "Create a Workspace 3"



## Create a sample C# project to post logs 

1. Start **Visual Studio 2019**.
2. Create a **New Project**.
3. Choose or search for C# Console App (.Net Framework) and then click **Next**.

Note: Ensure you select .NET Framework option and not .NET Core to ensure compatibility with this sample.

![Create Project][Create Project]

[Create Project]: https://github.com/achance/GAB19/blob/master/Screenshots/CreateProject.PNG?raw=true "Create Project"

4. Give your project a name. You can keep the default selections for the other options. Click **Create**.
5. Navigate to the sample repo on [GitHub] (https://github.com/achance/GAB19). Find the **Program.cs** file.
6. Copy the code in the sample file, and replace the code inside your project's **Program.cs** file.
7. You will need to replace 2 values in the code with values from your Azure Log Analytics Workspace. Locate the variables in the file for **customerID** and **sharedKey**. 

![Replace Lines][Replace Lines]

[Replace Lines]: https://github.com/achance/GAB19/blob/master/Screenshots/ReplaceLines.PNG?raw=true "Replace Lines"

8. Go back to the Azure Portal to get the values for your workspace. Ensure you have your Log Analytics Workspace selected. 
9. Select **Advanced Settings** from the menu on the left. 
10. Find the values under **Connected Sources** **-->** **Windows Servers** for **WORKSPACE ID** and PRIMARY KEY**. 

![Workspace][Workspace]

[Workspace]: https://github.com/achance/GAB19/blob/master/Screenshots/SharedKey.PNG?raw=true "Workspace"

11. Replace the value for the **customerID** variable with the **WORKSPACE ID** value. 
12. Replace the value for the **sharedKey** variable with the **PRIMARY KEY** value. 
13. Save the project and run the app. You should see "OK" logged to the console. This confirms that the log request was sent successfully. 


## Query for your sample logs in Azure 

To query for the logs, we will use the [Kusto Query language] (https://docs.microsoft.com/en-us/azure/kusto/query/index). 

1. From the Azure Portal, ensure that you still have your workspace selected. 
2. Select **Logs** from the menu on the left. Then expand the schema for **Custom Logs**. 

![Query] [Query]

[Query]: https://github.com/achance/GAB19/blob/master/Screenshots/Query.PNG?raw=true "Query"

3. Query for your newly created logs using the following sample.


```
TestLog1_CL
| where testProp_s != ""
```

4. Copy or enter the query into Query pane, and then click **Run**. You should see results displayed in the table similar to below.

![QueryResults] [QueryResults]

[QueryResults]: https://github.com/achance/GAB19/blob/master/Screenshots/QueryResults.PNG?raw=true "QueryResults"


For a real world scenario, you will likely need to write more complex queries. A log query can also be used with Azure Monitor Alerts.

Here are a few query examples:

You can control what columns are presented by using the **Project** operator.

```
TestLog1_CL
| where testProp_s != ""
| project TimeGenerated , testProp_s ,Type
```

Filter on a custom time range.

```
TestLog1_CL
| where testProp_s != ""
| where TimeGenerated > ago(12h)
| project TimeGenerated , testProp_s ,Type
```

Summarize your data with operators like **Count**

```
TestLog1_CL
| where testProp_s != ""
| where TimeGenerated > ago(12h)
| count 

```



## Create an alert based on your logs

1. With your workspace still selected, click on the **Alerts** option under **Monitoring**.

![Alerts1] [Alerts1]

[Alerts1]: https://github.com/achance/GAB19/blob/master/Screenshots/Alerts1.PNG?raw=true "Alerts1"


2. Click on **New Alert Rule**.

![Alerts2] [Alerts2]

[Alerts2]: https://github.com/achance/GAB19/blob/master/Screenshots/Alerts2.PNG?raw=true "Alerts2"

3. Under **Condition**, click **Add Condition**. For the signal type, select **Custom Log Search**. 

![Alerts3] [Alerts3]

[Alerts3]: https://github.com/achance/GAB19/blob/master/Screenshots/Alerts3.PNG?raw=true "Alerts3"


4. For this example, paste the following into the **Search Query** pane. 

```
TestLog1_CL
| where testProp_s == "Error"
```

5. Under **Alert logic**, change the **Threshold value** to **0**. Leave all the other settings at their default. 
When finished, your setup should look similar to this:

![Alerts4] [Alerts4]

[Alerts4]: https://github.com/achance/GAB19/blob/master/Screenshots/Alerts4.PNG?raw=true "Alerts4"

Click **Done** to complete the condition setup. 

The alert requires an **Action group** to direct where the alerts will be sent to. 

6. Click on **Create New** under **Action Groups**. Fill out the required info, and add an action for **Email**.
To properly test, enter a valid email address. Click **OK** to complete the setup. 

![Action Group] [Action Group]

[Action Group]: https://github.com/achance/GAB19/blob/master/Screenshots/ActionGroup.PNG?raw=true "Action Group"

Note: A confirmation email will be sent notifying that email address was added to the action group.

7. You should now see your newly created Action Group selected. Finish the alert details. 

![Action Group 2] [Action Group 2]

[Action Group 2]: https://github.com/achance/GAB19/blob/master/Screenshots/ActionGroup2.PNG?raw=true "Action Group 2"


Click **Create Alert Rule** to complete the setup. 



## Test the alert with the logs program

1. Open the Console app in Visual Studio and go back to the Program.cs file.
2. Find the line of code in the main method that has the **SendLog** function.
3. Replace the "testValue" with "Error", or copy the code below to overwrite it.

```C#

  string result = SendLog(customerID, sharedKey, "TestLog1", @"[{""testProp"": ""Error""}]");
```

4. Run the program again, and ensure that you see "OK" in the console output. 

This should trigger the alert based on our configuration.

Note: The default alert configuration had a time interval of 5 minutes. It may take 5 minutes or longer to see the alert notification depending on the timing. 

We can confirm the log was written with the "Error" value by running a query.

5. Navigate back to the logs workspace, and select **Logs** from the menu. 
6. Enter the following in the query pane:

```
TestLog1_CL
| where testProp_s == "Error"

```

Confirm that you see the result for your "Error" log.

















