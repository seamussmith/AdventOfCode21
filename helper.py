#!/bin/python
import sys, os, re

def dotnet_new(template: str, name: str):
    os.system(f"dotnet new {template} --name {name}")

def dotnet_sln_add(solution: str, proj_dir: str):
    os.system(f"dotnet sln {solution} add {proj_dir}")

def dotnet_add_ref(proj1: str, proj2: str):
    os.system(f"dotnet add ./{proj1} reference ./{proj2}")

proj_name = re.split("/|\\\\", os.getcwd())[-1]
day = sys.argv[1]

sln=f"./{proj_name}.sln"

project=f"{proj_name}.Day{day}"
project_csproj=f"./{project}/{project}.csproj"

project_test=f"{proj_name}.Day{day}.Test"
project_test_csproj=f"./{project_test}/{project_test}.csproj"

template_data = {
    "day": day,
    "proj_name": proj_name
}

# Get the templates for the classes
# Calling open without closing causes a resource leak. Whatever....
solution_template = open("./Templates/SolutionTemplate", "r").read()
test_template = open("./Templates/TestTemplate", "r").read()

if not os.path.isfile(f"./{proj_name}.sln"):
    dotnet_new("sln", proj_name)

# Create the projects
dotnet_new("classlib", project)
dotnet_new("xunit", project_test)
os.remove(f"./{project}/Class1.cs")
os.remove(f"./{project_test}/UnitTest1.cs")

# Reference the projects in the solution
dotnet_sln_add(sln, project_csproj)
dotnet_sln_add(sln, project_test_csproj)

# Have the test project reference the project
dotnet_add_ref(project_test, project)

# Write the templates
# Wow! Even more resource leaks! How wonderful!
open(f"./{project}/Solution.cs", "x").write(solution_template % (template_data | {"namespace": project}))
open(f"./{project_test}/Test.cs", "x").write(test_template % (template_data | {"namespace": project_test}))

# Create some files for the puzzle inputs
os.makedirs(f"./{project_test}/Inputs/", exist_ok=True)
# oh OH OHHH HE IS MAKING THE RESOURCES LEAK AGAIN OH LOOK AT EM GO ALL THE RESOURCES YEAAA BOI! HTOP GIVING ALL FULL RED BARS WHOA!
open(f"./{project_test}/Inputs/testinput.txt", "w+")
open(f"./{project_test}/Inputs/puzzleinput.txt", "w+")
