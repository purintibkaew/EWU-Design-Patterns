// Created by Jonathan Lewis, Jacob Schwartz, Ian Davies

public class CourseTester {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		Course course = new Course();
		
		//nothing added to the list, so this proves that error checking is correct
		for(Student s : course)
		{
			System.out.println(s);
		}
		
		course.addStudent(new Student("Bob"));
		course.addStudent(new Student("Claire"));
		course.addStudent(new Student("billy"));
		course.addStudent(new Student("Bobby"));

		
		for(Student s : course)
		{
			System.out.println(s);
		}
	}

}
