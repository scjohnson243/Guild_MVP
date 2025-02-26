graph TD
    A[Start] --> B[Pull from remote main branch]
    B --> C[Create a new branch for the task]
    C --> D[Make changes, stage, commit]
    D --> E{Is the task complete?}
    E -- No --> D
    E -- Yes --> F[Push branch to remote]
    F --> G[Create pull request on GitHub]
    G --> H[Wait for review and approval]
    H --> I[Merge pull request]
    I --> J[Pull updated main branch]
    J --> K[Delete feature branch]
    K --> L[End]
