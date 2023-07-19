#!/bin/bash

UNITY_PATH="/c/Program Files/Unity/Hub/Editor/2022.2.18f1/Editor/Unity.exe"
LOG_FILE="/c/ProgramData/Jenkins/.jenkins/workspace/GameFuncPractice/Build/Log/BuildLog.txt"
PROJECT_PATH="/c/ProgramData/Jenkins/.jenkins/workspace/GameFuncPractice/GameFuncPractice"
BUILD_METHOD="Build.BuildForWindows"
BUILD_TARGET="StandaloneWindows64"
BUILD_PATH="/c/ProgramData/Jenkins/.jenkins/workspace/GameFuncPractice/Build/Build"
PYTHON_SCRIPT_PATH="/c/ProgramData/Jenkins/.jenkins/workspace/GameFuncPractice/py/upload_to_drive.py"

echo StartUnityBUild
"$UNITY_PATH" -batchmode -nographics -silent-crashes -quit -logFile "$LOG_FILE" -projectPath "$PROJECT_PATH" -executeMethod "$BUILD_METHOD" -buildTarget "$BUILD_TARGET" -CustomArgs:buildPath=$BUILD_PATH -CustomArgs:developBuild=$DEVELOP_BUILD
echo EndUnityBuild
