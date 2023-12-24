using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class SlackIntegrationBuildNotifier : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report.summary.result == BuildResult.Succeeded)
        {
            SlackIntegration notifier = new SlackIntegration();
            notifier.OnBuildComplete("Build completed successfully!" + "Build Path: " + report.summary.outputPath);
        }
    }
}
