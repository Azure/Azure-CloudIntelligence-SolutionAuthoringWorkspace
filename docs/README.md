# How to contribute

## Background
The documentation is hosted on [GitHub pages](https://help.github.com/categories/github-pages-basics/) using [Jekyll](https://help.github.com/articles/about-github-pages-and-jekyll/) as a static site generator.

The website automatically created from the sources files found in this directory is available at [https://azure.github.io/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/](https://azure.github.io/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/).

## Contributing

### 1. Fork this repository into your private GitHub account
### 2. Update GitHub Pages repository settings
Set **Source** to **master branch /docs folder**:

![](images/github-pages-settings.jpg)

This will result in the creation of a private SAW Documentation website (e.g., https://wdecay.github.io/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/).

### 3. Edit existing or add new Markdown files

This can be done either directly on GitHub by using the [rich file management features](https://help.github.com/categories/managing-files-in-a-repository/), or by cloning your private repo and making the changes locally.

Changes pushed into the master branch of your private repo are immediately reflected in the GitHub Pages website created earlier.

### 4. Open a pull request across forks

![](images/github-pull-request.jpg)

## Advanced editing

git clone https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace

```
sudo apt-get update
sudo apt-get upgrade
sudo apt-get install ruby ruby-dev make g++
sudo gem install bundler
```

cd Azure-CortanaIntelligence-SolutionAuthoringWorkspace/docs/
bundle install 

