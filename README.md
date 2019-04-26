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

[CreateProject]: https://github.com/achance/GAB19/blob/master/Screenshots/CreateProject.PNG?raw=true "Create Project"

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









