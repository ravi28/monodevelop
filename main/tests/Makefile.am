include $(top_srcdir)/xbuild.include

SUBDIRS=UnitTests MacPlatform.Tests UserInterfaceTests TestRunner Ide.Tests

MONO=mono
RUN_TEST=$(MDTOOL_RUN) run-md-tests
TEST_DIR=$(top_builddir)/build/tests
EXTERNAL=$(top_builddir)/external
MACOS_10_10=$(shell (test `sw_vers -productVersion | cut -f1 -d.` = 10 && test `sw_vers -productVersion | cut -f2 -d.` -ge 10) && echo yes)

TEST_PROJECTS_MAC = \
	MacPlatform.Tests/MacPlatform.Tests.csproj

TEST_PROJECTS_COMMON = \
	UnitTests/UnitTests.csproj \
	$(top_srcdir)/src/addins/MonoDevelop.Debugger/MonoDevelop.Debugger.Tests/MonoDevelop.Debugger.Tests.csproj

TEST_ASSEMBLIES_MAC = \
	$(TEST_DIR)/MonoDevelop.VersionControl.Subversion.Tests.dll \
	$(TEST_DIR)/MacPlatform.Tests.dll

TEST_ASSEMBLIES_WINDOWS = \
	$(TEST_DIR)/VersionControl.Subversion.Win32.Tests.dll

TEST_ASSEMBLIES_COMMON = \
	$(TEST_DIR)/Ide.Tests.dll \
	$(TEST_DIR)/MonoDevelop.VersionControl.Git.Tests.dll \
	$(TEST_DIR)/UnitTests.dll \
	$(TEST_DIR)/MonoDevelop.Debugger.Tests.dll \
	$(TEST_DIR)/Mono.TextTemplating.Tests.dll \
	$(TEST_DIR)/MonoDevelop.PackageManagement.Tests.dll \
	$(TEST_DIR)/MonoDevelop.TextEditor.Tests.dll \
	$(TEST_DIR)/MonoDevelop.Xml.Tests.dll \
	$(TEST_DIR)/MonoDevelop.AspNet.Tests.dll \
	$(EXTERNAL)/nrefactory/bin/Debug/ICSharpCode.NRefactory.Tests.dll

ALL_CSPROJ = $(TEST_PROJECTS_COMMON)
TEST_ASSEMBLIES = $(TEST_ASSEMBLIES_COMMON)

if ENABLE_MACPLATFORM
ALL_CSPROJ += $(TEST_PROJECTS_MAC)
TEST_ASSEMBLIES += $(TEST_ASSEMBLIES_MAC)
endif

if ENABLE_WINDOWSPLATFORM
TEST_ASSEMBLIES += $(TEST_ASSEMBLIES_WINDOWS)
endif

UITEST_ASSEMBLIES = \
	$(TEST_DIR)/UserInterfaceTests.dll

test:
	@if test -n "$(assembly)"; then \
		for asm in $(TEST_ASSEMBLIES); do \
			if test `basename $$asm` = $(assembly); then \
				($(RUN_TEST) -xml=TestResult_`basename $$asm`.xml -labels $$asm) || exit $?; \
			fi; \
		done; \
	fi

	@if ! test -n "$(assembly)"; then \
		fail=0; \
		for asm in $(TEST_ASSEMBLIES); do \
			echo Testing `basename $$asm`...; \
			($(RUN_TEST) -xml=TestResult_`basename $$asm`.xml -labels $$asm); \
			if [ $$? != 0 ]; then \
				fail=1; \
			fi; \
		done; \
		if [ $${fail} = 1 ]; then \
			exit 1; \
		fi; \
	fi

uitest:
	@if test -n "$(assembly)"; then \
		for asm in $(UITEST_ASSEMBLIES); do \
			if test `basename $$asm` = $(assembly); then \
				($(RUN_TEST) -xml=TestResult_`basename $$asm`.xml -labels $$asm) || exit $?; \
			fi; \
		done; \
	fi

	@if ! test -n "$(assembly)"; then \
		echo "You must pass 'assembly' argument"; \
		exit 1; \
	fi

coverage:
	if ! test -n "$(assembly)"; then \
		for asm in $(TEST_ASSEMBLIES); do \
			echo Generating coverage for `basename $$asm`...; \
			($(MD_LAUNCH_SETUP) exec -a "mdtool" $(RUNTIME) --debug --profile=log:coverage,onlycoverage,output=CoverageResult_`basename $$asm`.mlpd,ffile=`basename $$asm`.filter "$(MD_BIN_PATH)/mdtool.exe" run-md-tests $$asm); \
			mprof-report --coverage-out=CoverageResult_`basename $$asm`.xml CoverageResult_`basename $$asm`.mlpd > /dev/null; \
		done; \
	fi

include $(top_srcdir)/Makefile.include

EXTRA_DIST = \
	test-projects/resources-tester/ResourcesTester/Subfolder/FormFile2.cs \
	test-projects/resources-tester/ResourcesTester/Subfolder/FormFile2.Designer.cs \
	test-projects/resources-tester/ResourcesTester/Subfolder/Normal2.Designer.cs \
	test-projects/resources-tester/ResourcesTester/Subfolder/Normal2.resx \
	test-projects/resources-tester/ResourcesTester/Subfolder/Cultured2.ca.resx \
	test-projects/resources-tester/ResourcesTester/Subfolder/BitmapCultured2.ca.bmp \
	test-projects/resources-tester/ResourcesTester/Subfolder/Bitmap2.bmp \
	test-projects/resources-tester/ResourcesTester/Subfolder/FormFile2.ca.resx \
	test-projects/resources-tester/ResourcesTester/Subfolder/FormFile2.resx \
	test-projects/resources-tester/ResourcesTester/Bitmap1.bmp \
	test-projects/resources-tester/ResourcesTester/Normal.resx \
	test-projects/resources-tester/ResourcesTester/Normal.Designer.cs \
	test-projects/resources-tester/ResourcesTester/FormFile.Designer.cs \
	test-projects/resources-tester/ResourcesTester/Cultured.ca.resx \
	test-projects/resources-tester/ResourcesTester/Properties/Settings.Designer.cs \
	test-projects/resources-tester/ResourcesTester/Properties/Settings.settings \
	test-projects/resources-tester/ResourcesTester/Properties/Resources.resx \
	test-projects/resources-tester/ResourcesTester/Properties/AssemblyInfo.cs \
	test-projects/resources-tester/ResourcesTester/Properties/Resources.Designer.cs \
	test-projects/resources-tester/ResourcesTester/ResourcesTester.csproj \
	test-projects/resources-tester/ResourcesTester/FormFile.ca.resx \
	test-projects/resources-tester/ResourcesTester/BitmapCultured.ca.bmp \
	test-projects/resources-tester/ResourcesTester/FormFile.cs \
	test-projects/resources-tester/ResourcesTester/FormFile.resx \
	test-projects/resources-tester/ResourcesTester/Program.cs \
	test-projects/resources-tester/ResourcesTester.sln \
	test-projects/test-build-configs/Lib3/MyClass.cs \
	test-projects/test-build-configs/Lib3/Lib3.csproj \
	test-projects/test-build-configs/Lib3/Properties/AssemblyInfo.cs \
	test-projects/test-build-configs/Lib4/Lib4.csproj \
	test-projects/test-build-configs/Lib4/MyClass.cs \
	test-projects/test-build-configs/Lib4/Properties/AssemblyInfo.cs \
	test-projects/test-build-configs/Lib2/MyClass.cs \
	test-projects/test-build-configs/Lib2/Lib2.csproj \
	test-projects/test-build-configs/Lib2/Properties/AssemblyInfo.cs \
	test-projects/test-build-configs/test-build-configs.sln \
	test-projects/test-build-configs/Lib1/Lib1.csproj \
	test-projects/test-build-configs/Lib1/MyClass.cs \
	test-projects/test-build-configs/Lib1/Properties/AssemblyInfo.cs \
	test-projects/generated-console-project/TestProject.csproj \
	test-projects/generated-console-project/TestSolution.sln \
	test-projects/console-project-with-makefile/Makefile.include \
	test-projects/console-project-with-makefile/ConsoleProject/ConsoleProject.csproj \
	test-projects/console-project-with-makefile/ConsoleProject/Makefile \
	test-projects/console-project-with-makefile/ConsoleProject/consoleproject.in \
	test-projects/console-project-with-makefile/ConsoleProject/Properties/AssemblyInfo.cs \
	test-projects/console-project-with-makefile/ConsoleProject/Program.cs \
	test-projects/console-project-with-makefile/Makefile \
	test-projects/console-project-with-makefile/rules.make \
	test-projects/console-project-with-makefile/configure \
	test-projects/console-project-with-makefile/ConsoleProject.sln \
	test-projects/console-project/ConsoleProject/ConsoleProject.csproj \
	test-projects/console-project/ConsoleProject/Properties/AssemblyInfo.cs \
	test-projects/console-project/ConsoleProject/Program.cs \
	test-projects/console-project/ConsoleProject.sln

