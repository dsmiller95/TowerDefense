const util = require("util");
const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});
const question = util.promisify(rl.question).bind(rl);

const { execSync } = require("child_process");

const branches = [
  {
    name: "0.1-game-models",
    changedFiles: "",
  },
  {
    name: "0.2-turret-aiming",
    changedFiles:
      "Assets/scripts/PointTowardsComponent.cs Assets/scripts/PickTargetComponent.cs Assets/scripts/EnemyComponent.cs",
  },
  {
    name: "0.2.1-bullets",
    changedFiles:
      "Assets/scripts/BulletSpawnComponent.cs Assets/scripts/DamageComponent.cs Assets/scripts/HealthComponent.cs Assets/scripts/MoveComponent.cs",
  },
  {
    name: "0.3-enemy-spawning",
    changedFiles: "Assets/scripts/SpawnComponent.cs",
  },
  {
    name: "0.4-player-health",
    changedFiles:
      "Assets/scripts/DamageComponent.cs Assets/scripts/GameOverController.cs Assets/scripts/health/HealthComponent.cs Assets/scripts/health/IHealthy.cs Assets/scripts/health/ScriptableHealthComponent.cs Assets/scripts/scriptableObjects/FloatTextDisplay.cs Assets/scripts/scriptableObjects/FloatVariable.cs",
    postScript: "rm Assets/scripts/HealthComponent.cs",
  },
  {
    name: "0.5-turret-placement",
    changedFiles:
      "Assets/scripts/PlaceAtCursorComponent.cs Assets/scripts/actions/SpawnAction.cs",
  },
  {
    name: "0.6-gold",
    changedFiles:
      "Assets/scripts/actions/SpawnAction.cs Assets/scripts/events/LifecycleEvents.cs Assets/scripts/scriptableObjects/FloatVariable.cs",
  },
  {
    name: "0.7-scaling",
    changedFiles: "Assets/scripts/SpawnComponent.cs",
  },
];

async function DoWorkshop() {
  try {
    console.clear();
    execSync("git clone https://github.com/dsmiller95/TowerDefense.git");
    process.chdir("./TowerDefense");
    branches.forEach((branch) => {
      execSync(`git checkout ${branch.name}`);
    });
    execSync(`git checkout ${branches[0].name}`);
    execSync(`git checkout ${branches[1].name} -- ${branches[1].changedFiles}`);
    console.log("repository initialized in TowerDefense");

    let workingBranch = 1;
    for (; workingBranch < branches.length - 1; workingBranch++) {
      const branch = branches[workingBranch];
      await StepNext(workingBranch);
    }

    console.log("");
    console.log("-------------------------------------------");
    console.log(`Working on step "${branches[workingBranch].name}"`);
    console.log("-------------------------------------------");
    console.log("This is the last step! exiting the console app.");
    process.exit(0);
  } catch (e) {
    console.error("error encountered");
    console.error(e);
  }
}

async function StepNext(currentStep) {
  const currentStepBranch = branches[currentStep];
  let shouldFresh = await ShouldCheckoutFresh(currentStepBranch.name);
  execSync(`git add . && git commit -m "step ${currentStepBranch.name}"`);
  if (shouldFresh) {
    execSync(`git checkout ${currentStepBranch.name}`);
  }
  const nextStepBranch = branches[currentStep + 1];
  execSync(
    `git checkout ${nextStepBranch.name} -- ${nextStepBranch.changedFiles}`
  );
  if (nextStepBranch.postScript) {
    execSync(nextStepBranch.postScript);
  }
}
async function ShouldCheckoutFresh(currentStep) {
  console.log("");
  console.log("-------------------------------------------");
  console.log(`Working on step "${currentStep}"`);
  console.log("-------------------------------------------");
  console.log("Exercise continue options:");
  console.log("Enter Keep to Keep your changes and move to the next step");
  console.log(
    "Enter Fresh to checkout a Fresh branch and move on to the next step"
  );
  while (true) {
    let option = await question("How would you like to continue? ").catch(
      (x) => x
    );
    option = option.toLowerCase();
    switch (option) {
      case "keep":
      case "k":
        console.log("keeping changes and checking out new scripts");
        return false;
      case "fresh":
      case "f":
        console.log("committing changes and checking out fresh");
        return true;
      default:
        console.log(`unrecognized option "${option}"`);
        break;
    }
  }
}

DoWorkshop();
