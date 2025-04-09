**Project Plan: Automated Homework Prioritization System**

## Final Project Rubric and Specs

---


### Program Specification

Think of a problem that people have that could be solved with programming, similar to the approach we have taken this semester. Write a program that uses the principles of Programming with Classes to help solve this problem.

You are welcome to explore and use any libraries you would like, such as graphics, networking, Web, etc. Just make sure you get a chance to show that you are using the principles of this class. (You cannot use Unity or other frameworks like that, because you won't be able to demonstrate that you know how to write the code we have learned this semester.)
Guidelines

To be eligible for full credit, your program must:

    - Perform an interesting task or function.
    - Be completely written by you (it cannot simply add to an existing project.)
    - Be written in C# (and not in a "low code" environment such as Unity).
    - Use at least 8 classes.
    - Demonstrate the principle of abstraction.
        Classes should have specific responsibilities and collaborate with each other.
    - Demonstrate the principle of encapsulation.
        - Internal details should be private, external methods should be available for public behaviors.
    - Demonstrate the principle of inheritance.
        - Code should not be duplicated that could be place in a base class.
    - Demonstrate the principle of polymorphism.
        - Methods that are unique to a derived class should be defined in a base class, and overridden in a derived class.

*If you feel the best design for your program does not require inheritance or polymorphism, you should clearly demonstrate your understanding of these principles by explaining the design decisions that led you to this decision.*

---

### **Rubric**

| **Criteria** | **Ratings** | **Points** |
|-------------|-------------|------------|
| **Principle: Abstraction** | - **Complete (20 pts)**: Program is divided into classes with a single responsibility. - **Developing (17 pts)**: Uses classes, but some are too large and need splitting. - **Incomplete (0 pts)**: Minimal use of classes. | 20 pts |
| **Principle: Encapsulation** | - **Complete (20 pts)**: All member variables are private (or protected if needed); public methods expose necessary behavior. - **Developing (17 pts)**: Most member variables are private. - **Incomplete (0 pts)**: Public member variables accessed throughout the program. | 20 pts |
| **Principle: Inheritance** | - **Complete (20 pts)**: Shared behaviors/attributes are in a base class and inherited. - **Developing (17 pts)**: Inheritance is used. - **Incomplete (0 pts)**: No inheritance used. | 20 pts |
| **Principle: Polymorphism** | - **Complete (20 pts)**: Method overriding is used where appropriate. - **Developing (17 pts)**: At least one method is correctly overridden. - **Incomplete (0 pts)**: No method overriding used. | 20 pts |
| **Functionality: Program Runs** | - **Complete (20 pts)**: <ul><li>Open-ended: Runs with no runtime errors.</li><li>Foundation 4: All four programs run.</li></ul> - **Nearly Complete (18 pts)**: Some occasional runtime errors or 3/4 programs run. - **Developing (10 pts)**: Frequent errors or 2/4 programs run. - **Attempted (5 pts)**: One program runs. - **Incomplete (0 pts)**: Program doesn’t run. | 20 pts |
| **Functionality: Program Can Be Played** | - **Complete (80 pts)**: <ul><li>Open-ended: Fully functional.</li><li>Foundation 4: All four programs work.</li></ul> - **Nearly Complete (67 pts)**: Core functionality or 3 programs work. - **Developing (40 pts)**: Some user interaction or 2 programs work. - **Attempted (20 pts)**: Output resembles desired output or 1 program works. - **Incomplete (0 pts)**: Minimal interaction or functionality. | 80 pts |
| **Style: Whitespace** | - **Complete (10 pts)**: Fewer than 3 whitespace errors. - **Nearly Complete (7.5 pts)**: 3–6 whitespace errors. - **Developing (5 pts)**: More than 6 whitespace errors. - **Incomplete (0 pts)**: Very poor formatting. | 10 pts |
| **Style: Naming Conventions** | - **Complete (10 pts)**: TitleCase for classes/methods, _underscoreCamelCase for members, camelCase for locals. - **Nearly Complete (7.5 pts)**: Some naming errors (3–6). - **Developing (5 pts)**: Many errors. - **Incomplete (0 pts)**: Naming is inconsistent. | 10 pts |
| **Total Points** | | **200** |

---

# MY PLAN

## **1. Project Overview**

### **Objective:**

Develop a C# program that automates homework prioritization by integrating with Canvas, dynamically estimating task durations, and scheduling assignments based on impact, urgency, and personal adjustments.

### **Key Features:**

- **Canvas API Integration**: Automatically fetch assignments, due dates, point values, grades, and assignment categories with weightings.
- **Dynamic Task Duration Estimation**: Predict how long tasks will take based on historical data or assignment type.
- **Task Prioritization Algorithm**: Rank tasks based on grade impact, lateness, urgency, and user-defined factors.
- **Scheduling Integration**: Export tasks to a calendar (Google Calendar, Motion API, or an internal scheduler).
- **User Adjustments**: Allow manual priority overrides and custom weight tuning.

## **2. System Design**

### **2.1 Data Flow**

1. Fetch assignments from Canvas API.
2. Fetch assignment groups (categories) and weightings.
3. Estimate task durations based on past performance or predefined rules.
4. Compute priority scores using a ranking formula incorporating grading weights.
5. Store tasks in a JSON file.
6. Schedule high-priority tasks into a calendar or to-do system.
7. Allow user modifications via CLI, GUI, or web dashboard.

### **2.2 Components & Technologies**

| Component                   | Technology                                                        |
| --------------------------- | ----------------------------------------------------------------- |
| Programming Language        | C#                                                                |
| Data Storage                | JSON                                                             |
| API Integration             | Canvas API, Google Calendar API (optional), Motion API (optional) |
| Machine Learning (Optional) | ML.NET for task duration prediction                               |
| User Interface              | CLI, WPF/WinForms, or Web (ASP.NET)                               |
| Task Scheduling             | Custom logic or external API (Google Calendar, Motion)            |

## **3. Implementation Plan**

### **3.1 Phase 1: Canvas API Integration**

- Register API key for Canvas.
- Write C# client to fetch assignments & grades.
- Fetch assignment categories and their weightings.
- Parse and store data locally in JSON.

### **3.2 Phase 2: Task Duration Estimation [OPTIONAL]**

- Implement predefined duration estimates (e.g., quizzes = 1hr, essays = 3hrs).
- Store historical task completion times in JSON for adaptive learning.
- Use a structured JSON format to maintain past durations:
  ```json
  {
    "task_durations": {
      "Quizzes": [45, 50, 40, 60], 
      "Essays": [120, 150, 130, 140],
      "Programming HW": [180, 200, 220, 190]
    }
  }
  ```
- Estimate task durations dynamically using the **average of past durations** per category.
- Implement **rolling data retention** to keep only recent durations.

### **3.3 Phase 3: Task Prioritization**

- Define priority formula based on:
  - Due Date (earlier = higher priority)
  - Grade Impact (high weight tasks first, using Canvas assignment group weightings)
  - Lateness Policies (penalty-based adjustments) [OPTIONAL]
  - User-defined weight factors
- Allow manual priority overrides.

### **3.4 Phase 4: Scheduling & Export**

- Develop local task scheduler.
- (Optional) Integrate with Google Calendar API or Motion API.
- Allow task rescheduling based on real-time priorities.

### **3.5 Phase 5: User Interface & Refinement**

- Build CLI or GUI for task management.
- Add logging and notifications for deadlines.
- Refine and test automation flows.

### **3.6 (Optional) Phase 6: Calendar Sync Integration (Low Priority)**

- Implement two-way integration with a calendar API (e.g., Google Calendar).
- Automatically create, update, and delete calendar events based on assignment data and scheduling logic.
- Allow rescheduling and priority changes in-app to reflect on the calendar.
- Support OAuth2 authentication for secure access.
- Useful for visual tracking and notifications of tasks, but not essential to core functionality.

## **4. Challenges & Considerations**

- **Canvas API Rate Limits:** Implement caching to reduce API calls.
- **Task Duration Accuracy:** Improve estimates over time with ML or user input.
- **User-Friendly Adjustments:** Ensure easy overrides and custom weight settings.
- **Scheduling Conflicts:** Handle rescheduling and workload balancing.
- **Grading Weights:** Ensure the program correctly factors assignment categories into prioritization.
- **JSON Performance:** Implement efficient data handling (e.g., in-memory loading, rolling retention) to prevent performance issues.

## **5. Next Steps**

1. Finalize project architecture and data model.
2. Set up Canvas API integration and fetch assignment group weights.
3. Implement task prioritization logic using weight-based calculations.
4. Implement JSON-based task duration storage and estimation.
5. Choose and integrate a scheduling system.
6. Develop user interface and testing framework.
7. (Optional) Implement calendar sync if time permits.

