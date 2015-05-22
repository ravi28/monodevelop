//
// NewProjectController.cs
//
// Author:
//       Manish Sinha <manish.sinha@xamarin.com>
//
// Copyright (c) 2015 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using MonoDevelop.Components.AutoTest;
using MonoDevelop.Ide.Commands;
using NUnit.Framework;
using System.Threading;
using Xwt;


namespace UserInterfaceTests
{
	public class NewProjectController
	{
		static AutoTestClientSession Session {
			get { return TestService.Session; }
		}

		public void Open ()
		{
			Session.ExecuteCommand (FileCommands.NewProject);
			Session.WaitForElement (c => c.Window ().Marked ("MonoDevelop.Ide.Projects.GtkNewProjectDialogBackend"));
		}

		public bool SelectTemplateType (string categoryRoot, string category)
		{
			return Session.SelectElement (c => c.TreeView ().Marked ("templateCategoriesTreeView").Model ("templateCategoriesListStore__Name").Contains (categoryRoot).NextSiblings ().Text (category));
		}

		public bool SelectTemplate (string kindRoot, string kind)
		{
			return Session.SelectElement (c => c.TreeView ().Marked ("templatesTreeView").Model ("templateListStore__Name").Contains (kindRoot).NextSiblings ().Text (kind));
		}

		public bool Next ()
		{
			return Session.ClickElement (c => c.Button ().Marked ("nextButton"));
		}

		public bool Previous ()
		{
			return Session.ClickElement (c => c.Button ().Marked ("previousButton"));
		}

		public bool SetProjectName (string projectName)
		{
			return Session.EnterText (c => c.Textfield ().Marked ("projectNameTextBox"), projectName);
		}

		public bool SetSolutionName (string solutionName)
		{
			return Session.EnterText (c => c.Textfield ().Marked ("solutionNameTextBox"), solutionName);
		}

		public bool SetSolutionLocation (string solutionLocation)
		{
			return Session.EnterText (c => c.Textfield ().Marked ("locationTextBox"), solutionLocation);
		}

		public bool CreateProjectInSolutionDirectory (bool projectInSolution)
		{
			return SetCheckBox ("createProjectWithinSolutionDirectoryCheckBox", projectInSolution);
		}

		public bool UseGit (GitOptions options)
		{
			var gitSelectionSuccess = SetCheckBox ("useGitCheckBox", options.UseGit);
			if (gitSelectionSuccess && options.UseGit)
				return gitSelectionSuccess && SetCheckBox ("createGitIgnoreFileCheckBox", options.UseGitIgnore);
			return gitSelectionSuccess;
		}

		public bool SetCheckBox (string checkBoxName, bool active)
		{
			return Session.ToggleElement (c => c.CheckButton ().Marked (checkBoxName), active);
		}
			
		public bool GetSensitivity (string widgetName)
		{
			AppResult[] results = Session.Query (c => c.Marked (widgetName).Sensitivity (true));
			return results.Length > 0;
		}

		public bool SelectProjectOptions (string optionRoot, string option)
		{
			Session.ExecuteCommand (ProjectCommands.ProjectOptions);
			return Session.SelectElement (c => c.TreeView ().Marked ("optionCategoriesTreeView").Model ("optionCategoriesTreeStore__Name").Contains (optionRoot).NextSiblings ().Text (option));
		}

		public void SelectGenerateXmlOutputCheckButton ()
		{
			Session.ToggleElement (c => c.CheckButton ().Marked ("generateXmlOutputCheckButton"), true);
			Session.ClickElement (c => c.Button ().Marked ("buttonOk"));
		}

		public bool CreateNewFile (string fileName)
		{
			Session.ExecuteCommand (FileCommands.NewFile);
			Session.EnterText (c => c.Textfield ().Marked ("nameEntry"), fileName);
			return Session.ClickElement (c => c.Button ().Marked ("okButton"));
		}

		public void NavigateToMethod ()
		{
			Session.ExecuteCommand (TextEditorCommands.DocumentEnd);
			for (int i = 0; i < 4; i++) {
				Session.ExecuteCommand (TextEditorCommands.LineUp);
			}
			Session.ExecuteCommand (TextEditorCommands.InsertNewLine);
		}

		public void EnterTextInEditor ()
		{
			Session.EnterText (c => c.Textfield ().Marked ("MonoDevelop.Ide.Gui.Document"), "Testing afasfasdgsdgasdgsadgs");
			Session.EnterText (c => c.Textfield ().Marked ("IdeApp.Workbench.ActiveDocument"), "Testing afasfasdgsdgasdgsadgs");
			Session.ExecuteCommand (ProjectCommands.AddReference);
		} 

	}
}

 