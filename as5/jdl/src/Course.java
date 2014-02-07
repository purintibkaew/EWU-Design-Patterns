// Created by Jonathan Lewis, Jacob Schwartz, Ian Davies


import java.util.ArrayList;
import java.util.Iterator;


public class Course implements Iterable<Student> {
	
	private ArrayList<Student> students;
	
	public Course()
	{
		this.students = new ArrayList<Student>();
	}
	
	public void addStudent(Student s)
	{
		this.students.add(s);
	}
	
	public class CourseIterator implements Iterator<Student>
	{
		private ArrayList<Student> students;
		private int index;
		
		public CourseIterator(ArrayList<Student> students)
		{
			this.students = students;
			this.index = 0;
		}
		
		@Override
		public boolean hasNext() {
			
			if(this.index < 0 || this.index >= this.students.size())
				return false;
			return true;
		}

		@Override
		public Student next() {
			//if(this.index >= this.students.size() || this.index < 0)
				//return null;
			
			Student s = this.students.get(this.index);
			this.index++;
			return s;
		}

		@Override
		public void remove() {
			this.students.remove(this.index-1);
		}
		
	}

	@Override
	public Iterator<Student> iterator() {
		return new CourseIterator(this.students);
	}
}
