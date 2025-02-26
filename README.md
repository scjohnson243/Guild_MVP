## Development Workflow

Below is the Git workflow for contributing to "Guild_MVP," including directions for using SourceTree and Git together.

```mermaid
graph TD
    A[Start] --> B[Pull from remote main branch]
    B --> C[Create a new branch for the task]
    C --> D[Make changes, stage, commit]
    D --> E[Push branch to remote]
    E --> F[Create pull request on GitHub]
    F --> G[Wait for review and approval]
    G --> H[Merge pull request]
    H --> I[Pull updated main branch]
    I --> J[Delete feature branch]
    J --> K[End]
