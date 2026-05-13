from abc import ABC, abstractmethod


# =========================================
# ABSTRACT CLASS (BLUEPRINT)
# =========================================
class Course(ABC) :

    def __init__(self, course_id, course_name, credit_hours, max_capacity):
        self.course_id = course_id
        self.course_name = course_name

        # Protected Attributes
        self.__credit_hours = credit_hours
        self.__max_capacity = max_capacity

    # =========================
# ENCAPSULATION
# =========================
    def get_credit_hours(self):
        return self.__credit_hours

    def set_credit_hours(self, hours):
        if hours > 0:
            self.__credit_hours = hours
        else:
            print("Invalid credit hours!")

    def get_max_capacity(self):
        return self.__max_capacity

    def set_max_capacity(self, capacity):
        if capacity > 0:
            self.__max_capacity = capacity
        else:
            print("Invalid capacity!")

    # =========================
# ABSTRACT METHODS
# =========================
    @abstractmethod
    def calculate_tuition(self):
        pass

    @abstractmethod
    def display_course_info(self):
        pass


# =========================================
# PRACTICAL LAB COURSE
# =========================================
class PracticalLab(Course) :

    COST_PER_HOUR = 300

    def __init__(self, course_id, course_name, credit_hours,
                 max_capacity, lab_equipment_fee):

        super().__init__(course_id, course_name,
                         credit_hours, max_capacity)

        self.lab_equipment_fee = lab_equipment_fee

    def calculate_tuition(self):

        base_cost = self.get_credit_hours() * self.COST_PER_HOUR

        total = base_cost + self.lab_equipment_fee

        return total

    def display_course_info(self):

        print(f"""
Course Type : Practical Lab
Course ID   : {self.course_id}
Course Name : {self.course_name}
Credit Hours: {self.get_credit_hours()}
Lab Fee     : ${self.lab_equipment_fee}
Capacity    : {self.get_max_capacity()}
""")


# =========================================
# THEORETICAL LECTURE COURSE
# =========================================
class TheoreticalLecture(Course) :

    COST_PER_HOUR = 300

    def __init__(self, course_id, course_name,
                 credit_hours, max_capacity,
                 lecture_hall_capacity):

        super().__init__(course_id, course_name,
                         credit_hours, max_capacity)

        self.lecture_hall_capacity = lecture_hall_capacity

    def calculate_tuition(self):

        base_cost = self.get_credit_hours() * self.COST_PER_HOUR

        technology_fee = base_cost * 0.05

        total = base_cost + technology_fee

        return total

    def display_course_info(self):

        print(f"""
Course Type : Theoretical Lecture
Course ID   : {self.course_id}
Course Name : {self.course_name}
Credit Hours: {self.get_credit_hours()}
Tech Fee    : 5%
Hall Seats  : {self.lecture_hall_capacity}
Capacity    : {self.get_max_capacity()}
""")


# =========================================
# STUDENT SCHEDULE
# =========================================
class StudentSchedule :

    def __init__(self):
        self.registered_courses = []

    def add_course(self, course):

        if course in self.registered_courses:
            print("Course already registered!")
        else:
            self.registered_courses.append(course)
            print(f"{course.course_name} added successfully!")

    def view_schedule(self):

        if not self.registered_courses:
            print("No courses registered yet.")
            return

        print("\n===== STUDENT SCHEDULE =====")

        for course in self.registered_courses:
            print(f"{course.course_id} - {course.course_name}"
                  f" ({course.get_credit_hours()} Credit Hours)")

    def print_tuition_bill(self):

        if not self.registered_courses:
            print("No registered courses.")
            return

        print("\n========== FINAL TUITION BILL ==========")

        total = 0

        for course in self.registered_courses:

            course_fee = course.calculate_tuition()

            print(f"{course.course_name:<25} ${course_fee}")

            total += course_fee

        print("----------------------------------------")
        print(f"TOTAL SEMESTER FEES:       ${total}")
        print("========================================")


# =========================================
# AVAILABLE COURSES
# =========================================
courses = [

    PracticalLab(
        "CS101L",
        "Programming Lab",
        3,
        25,
        150
    ),

    PracticalLab(
        "NET201L",
        "Networking Lab",
        2,
        20,
        120
    ),

    TheoreticalLecture(
        "MATH101",
        "Calculus",
        3,
        100,
        120
    ),

    TheoreticalLecture(
        "AI301",
        "Artificial Intelligence",
        4,
        80,
        90
    )
]


# =========================================
# MAIN SYSTEM
# =========================================
schedule = StudentSchedule()

while True:

    print("""
========================================
 Interactive University Registration
========================================

1. View Available Courses
2. Register for Course
3. View Student Schedule
4. Print Tuition Bill
5. Exit

========================================
""")

    choice = input("Enter your choice: ")

    # =====================================
# VIEW COURSES
# =====================================
    if choice == "1":

        print("\n===== AVAILABLE COURSES =====")

        for course in courses:
            course.display_course_info()

    # =====================================
# REGISTER COURSE
# =====================================
    elif choice == "2":

        user_input = input(
            "Enter Course ID or Course Name: "
        ).lower()

        found = False

        for course in courses:

            if (user_input == course.course_id.lower()
                    or user_input == course.course_name.lower()):

                schedule.add_course(course)

                found = True
                break

        if not found:
    print("Course not found!")

    # =====================================
# VIEW SCHEDULE
# =====================================
    elif choice == "3":

        schedule.view_schedule()

    # =====================================
# PRINT BILL
# =====================================
    elif choice == "4":

        schedule.print_tuition_bill()

    # =====================================
# EXIT
# =====================================
    elif choice == "5":

        print("Exiting system...")
        print("Thank you for using the system.")
        break

    # =====================================
# ERROR HANDLING
# =====================================
    else:

        print("Invalid input! Please enter a number from 1 to 5.")
