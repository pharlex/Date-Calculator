/*
 * Created by SharpDevelop.
 * User: Pharlex
 * Date: 3/26/2019
 * Time: 4:12 PM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace DateCalc
{
	//Creates a UI for 2 calculation modes one mode calculates the number of business days from the selected date changing information on this mode 
	//will alter the other selections if necessary besides number of busniess days
	//the other mode calculates the x day of the month with the year month and day of the week selected
	public partial class MainForm : Form
	{
		private DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
		private Calendar cal;
		
		//initialize boxes, buttons and labels
		private ComboBox month = new ComboBox();
		private ComboBox day = new ComboBox();
		private TextBox year = new TextBox();
		private TextBox days = new TextBox();
		private TextBox weeks = new TextBox();
		private TextBox months = new TextBox();
		private TextBox years = new TextBox();
		private ComboBox dOfWeek = new ComboBox();//needs implemented, reuse month and year
		private ComboBox xDay = new ComboBox();//needs implemented
		
		private Button calculate = new Button();//needs implemented, use for both modes
		private Button switchMode = new Button();
		private Button busMode = new Button();
		private Button xDaysMode = new Button();//needs createXDayMode implemented
		
		private Label busModeLabel = new Label();
		private Label xDaysModeLabel = new Label();
		private Label dateSelLabel = new Label();
		private Label monLabel = new Label();
		private Label yearLabel = new Label();
		private Label dayLabel = new Label();
		private Label addSubLabel = new Label();
		private Label daysLabel = new Label();
		private Label weeksLabel = new Label();
		private Label monthsLabel = new Label();
		private Label yearsLabel = new Label();
		private Label xDayLabel = new Label();
		private Label dOfWeekLabel = new Label();
		private Label calcXDayLabel = new Label();
		private Label calculation = new Label();
		
		private int curMode; //used for determining current selected mode
		
		public MainForm()
		{
			InitializeComponent();
			this.Height = 500;
			this.Width = 500;
			createModeSelect();
			
		}
		
		private void createModeSelect(){
			//set text for mode select screen buttons and labels
			busModeLabel.Text = "Calc Business Days";
			busMode.Text = "Select";
			xDaysModeLabel.Text = "X day of the month";
			xDaysMode.Text = "Select";
			
			//set locations for mode select screen buttons and labels
			busModeLabel.Location = new Point(10, 10);
			xDaysModeLabel.Location = new Point(10, 60);
			busMode.Location = new Point(30, 30);
			xDaysMode.Location = new Point(30, 80);
			
			//setup event handlers
			month.SelectedIndexChanged += 
				new System.EventHandler(month_SelectedIndexChanged);
			year.TextChanged += 
				new System.EventHandler(year_TextChanged);
			year.KeyPress += year_KeyPress;
			days.KeyPress += days_KeyPress;
			weeks.KeyPress += weeks_KeyPress;
			months.KeyPress += months_KeyPress;
			years.KeyPress += years_KeyPress;
			busMode.Click += new System.EventHandler(busMode_Click);
			xDaysMode.Click += new System.EventHandler(xDaysMode_Click);
			
			
			//initialize location, text and event handler for switch mode button and calculate button
			switchMode.Location = new Point(this.Width-100, this.Height-80);
			switchMode.Text = "Switch";
			switchMode.Click += new System.EventHandler(switchMode_Click);
			calculate.Location = new Point(10, this.Height-80);
			calculate.Text = "Calculate";
			calculate.Click += new System.EventHandler(calculate_Click);
			calculation.Location = new Point(10, this.Height-150);
			calculation.Size = new System.Drawing.Size(450, 39);
			
			Controls.Add(busMode);
			Controls.Add(xDaysMode);
			Controls.Add(busModeLabel);
			Controls.Add(xDaysModeLabel);
		}
		
		private void createBusMode(){
			//set label text
			dateSelLabel.Text = "Pick a Date";
			dayLabel.Text = "Day";
			monLabel.Text = "Month";
			yearLabel.Text = "Year";
			addSubLabel.Size = new System.Drawing.Size(220, 13);
			addSubLabel.Text = "Enter values to add use negative to subtract";
			daysLabel.Text = "Days";
			weeksLabel.Text = "Weeks";
			monthsLabel.Text = "Months";
			yearsLabel.Text = "Years";
			
			//set locations
			dateSelLabel.Location = new Point(10, 10);
			month.Location = new Point(60, 40);
			day.Location = new Point(60, 70);
			year.Location = new Point(60, 100);
			addSubLabel.Location = new Point(10, 130);
			days.Location = new Point(60, 160);
			weeks.Location = new Point(60, 190);
			months.Location = new Point(60, 220);
			years.Location = new Point(60, 250);
			
			monLabel.Location = new Point(10, 40);
			dayLabel.Location = new Point(10, 70);
			yearLabel.Location = new Point(10, 100);
			
			daysLabel.Location = new Point(10, 160);
			weeksLabel.Location = new Point(10, 190);
			monthsLabel.Location = new Point(10, 220);
			yearsLabel.Location = new Point(10, 250);
			
			//set initial values to todays date
			year.Text = DateTime.Now.Year.ToString();
			cal = dfi.Calendar;
			createMonth();
			month.SelectedItem = month.Items[DateTime.Now.Month-1];
			createDay(month.SelectedItem.ToString());
			day.SelectedIndex = DateTime.Now.Day-1;
			day.Text = DateTime.Now.Day.ToString();
			
			//display to gui
			Controls.Add(month);
			Controls.Add(year);
			Controls.Add(day);
			Controls.Add(days);
			Controls.Add(weeks);
			Controls.Add(months);
			Controls.Add(years);
			Controls.Add(dateSelLabel);
			Controls.Add(monLabel);
			Controls.Add(dayLabel);
			Controls.Add(yearLabel);
			Controls.Add(addSubLabel);
			Controls.Add(daysLabel);
			Controls.Add(weeksLabel);
			Controls.Add(monthsLabel);
			Controls.Add(yearsLabel);
		}
		
		private void createXDayMode(){
			
			//set label text
			calcXDayLabel.Size = new System.Drawing.Size(170, 13);
			calcXDayLabel.Text = "Select the day you wish to find";
			monLabel.Text = "Month";
			yearLabel.Text  = "Year";
			xDayLabel.Text = "x day of month";
			dOfWeekLabel.Text = "Day of week";
			
			//set initial values for boxes
			year.Text = DateTime.Now.Year.ToString();
			createMonth();
			month.Text = month.Items[DateTime.Now.Month-1].ToString();
			createDofWeek();
			dOfWeek.SelectedIndex = 0;
			createXDays();
			xDay.SelectedIndex = 0;
			
			//set locations
			calcXDayLabel.Location = new Point(10, 10);
			xDay.Location = new Point(90, 40);
			dOfWeek.Location = new Point(90, 70);
			month.Location = new Point(90, 100);
			year.Location = new Point(90, 130);
			xDayLabel.Location = new Point(10, 40);
			dOfWeekLabel.Location = new Point(10, 70);
			monLabel.Location = new Point(10, 100);
			yearLabel.Location = new Point(10, 130);
			
			//display to gui
			Controls.Add(xDay);
			Controls.Add(dOfWeek);
			Controls.Add(month);
			Controls.Add(year);
			Controls.Add(calcXDayLabel);
			Controls.Add(xDayLabel);
			Controls.Add(dOfWeekLabel);
			Controls.Add(monLabel);
			Controls.Add(yearLabel);
			
			
		}
		
		
		//event handler methods
		private void month_SelectedIndexChanged(object sender, 
		System.EventArgs e){
			createDay(month.GetItemText(month.SelectedItem));
		}
		
		private void year_TextChanged(object sender, 
		System.EventArgs e){
			createDay(month.GetItemText(month.SelectedItem));
		}
		
		
		
		private void switchMode_Click(object sender, 
		System.EventArgs e){
			Controls.Clear();
			Controls.Add(busMode);
			Controls.Add(xDaysMode);
			Controls.Add(busModeLabel);
			Controls.Add(xDaysModeLabel);
		}
		
		private void busMode_Click(object sender, 
		System.EventArgs e){
			Controls.Clear();
			Controls.Add(switchMode);
			curMode = 1;
			Controls.Add(calculate);
			createBusMode();
		}
		private void xDaysMode_Click(object sender, 
		System.EventArgs e){
			Controls.Clear();
			Controls.Add(switchMode);
			curMode = 2;
			Controls.Add(calculate);
			createXDayMode();
		}
		
		private void calculate_Click(object sender, 
		System.EventArgs e){
			if(curMode==1){
				//business mode
				int val;
				bool invalDate = false;
				if(int.TryParse(year.Text, out val)){
				   	
				}
				DateTime dt = new DateTime(0);
				try{
					dt = new DateTime(val, month.SelectedIndex+1, day.SelectedIndex+1);
				}
				catch(ArgumentOutOfRangeException){
					DialogResult = MessageBox.Show("Invalid year was entered", "Invalid year error", MessageBoxButtons.OK);
					invalDate = true;
				}
				int val2;
				if(days.Text == ""){
					val2 = 0;
				}
				else{
					if(int.TryParse(days.Text, out val2)){
				   		
					}
				}
				int val3;
				if(weeks.Text == ""){
					val3 = 0;
				}
				else{
					if(int.TryParse(weeks.Text, out val3)){
				   		
					}
				}
				int val4;
				if(months.Text == ""){
					val4 = 0;
				}
				else{
					if(int.TryParse(months.Text, out val4)){
				   		
					}
				}
				int val5;
				if(years.Text == ""){
					val5 = 0;
				}
				else{
					if(int.TryParse(years.Text, out val5)){
				   		
					}
				}
				if(invalDate){
					calculation.Text = "Invalid year was input";
				}
				else{
					DateTime calculatedDay = calculateDays(dt, val2, val3, val4, val5);
					if(calculatedDay.CompareTo( new DateTime(0))==0){
						calculation.Text = "Invalid date was added";
					}
					else{
						calculation.Text = "The number of business days from the date up to added values is " + businessDaysCalc(dt, val2, val3, val4, val5).ToString() + "\n";
						calculation.Text += "The day is " + calculatedDay.ToString("MM/dd/yyyy");
						string holiday = isHoliday(calculatedDay);
						string weekend = isWeekend(calculatedDay);
						if(holiday!=""){
							calculation.Text += " this is " + holiday + " which is a holiday";
						}
						if(weekend!=""){
							calculation.Text += " this is " + weekend + " which is a weekend";
						}
					}
				}
				Controls.Add(calculation);
			}
			if(curMode==2){
				//x days mode
				int val;
				if(int.TryParse(year.Text, out val)){
				   	
				}
				calculation.Text = "The " + xDay.SelectedItem.ToString() + " " +  dOfWeek.SelectedItem.ToString() + " of " 
					+ month.SelectedItem.ToString() + " " + val.ToString() + " is "
					+ numDayOfMonth(month.SelectedIndex+1, xDay.SelectedIndex+1, val, dOfWeek.SelectedIndex).ToString("dd");
				if(val>9999 || val<0){
					calculation.Text = "An invalid year was entered";
				}
				Controls.Add(calculation);
			}
		}
		
		private void year_KeyPress(object sender, KeyPressEventArgs e){
    
    		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
    		{
        		e.Handled = true;
    		}
		}
		private void days_KeyPress(object sender, KeyPressEventArgs e){
    
    		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
    		{
        		e.Handled = true;
    		}
		}
		
		private void weeks_KeyPress(object sender, KeyPressEventArgs e){
    
    		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
    		{
        		e.Handled = true;
    		}
		}
		
		private void months_KeyPress(object sender, KeyPressEventArgs e){
    
    		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
    		{
        		e.Handled = true;
    		}
		}
		
		private void years_KeyPress(object sender, KeyPressEventArgs e){
    
    		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
    		{
        		e.Handled = true;
    		}
		}
		
		//End of event handlers
		
		
		private void createDay(string selMonth){
			//add values to day box based on the month selected and year selected for february
			int i;
			int yearNum;
			day.Items.Clear();//empty for cases of recalling
			if(int.TryParse(year.Text, out yearNum)){
				
			}
			if(selMonth == "January" || selMonth == "March" || selMonth == "May" || selMonth == "July" ||
			   selMonth == "August" || selMonth == "October" || selMonth == "December"){
				for(i=1; i<=31; i++){
					day.Items.Add(i);
				}
			}
			else if(selMonth == "February"){
				if(yearNum%4==0){
					for(i=1; i<=29; i++){
						day.Items.Add(i);
					}
				}
				else{
					for(i=1; i<=28; i++){
						day.Items.Add(i);
					}
				}
			}
			else{
				for(i=1; i<=30; i++){
					day.Items.Add(i);
				}
			}
			int val;
			if(int.TryParse(day.Text, out val)){
				//reset value of day if it is too high for the month selected
				if(day.Items.IndexOf(val)==-1){
					day.SelectedIndex=i-2;
					day.Text=day.SelectedItem.ToString();
				}
				day.SelectedItem = val;
			}
			
			
		}
		
		private void createMonth(){
			//add values to the month box
			month.Items.Clear();//clear any previous calls
			month.Items.Add("January");
			month.Items.Add("February");
			month.Items.Add("March");
			month.Items.Add("April");
			month.Items.Add("May");
			month.Items.Add("June");
			month.Items.Add("July");
			month.Items.Add("August");
			month.Items.Add("September");
			month.Items.Add("October");
			month.Items.Add("November");
			month.Items.Add("December");
		}
		
		private void createDofWeek(){
			//add values to the month box
			dOfWeek.Items.Clear();//clear any previous calls
			dOfWeek.Items.Add("Sunday");
			dOfWeek.Items.Add("Monday");
			dOfWeek.Items.Add("Tuesday");
			dOfWeek.Items.Add("Wednesday");
			dOfWeek.Items.Add("Thursday");
			dOfWeek.Items.Add("Friday");
			dOfWeek.Items.Add("Saturday");
		}
		
		private void createXDays(){
			//add values to the month box
			xDay.Items.Clear();//clear any previous calls
			xDay.Items.Add("First");
			xDay.Items.Add("Second");
			xDay.Items.Add("Third");
			xDay.Items.Add("Fourth");
			xDay.Items.Add("Last");
		}
		
		private string isWeekend(DateTime date){
			if((int)date.DayOfWeek==0){
				return "Sunday";
			}
			if((int)date.DayOfWeek==6){
				return "Saturday";
			}
			return "";
		}
		
		
		private string isHoliday(DateTime date){
			int curDay = date.Day;
			int curMon = date.Month;
			int curYear = date.Year;
			if(curMon==1 && curDay==1){
				return "New Years Day";
			}
			else if(curMon==1 && curDay==numDayOfMonth(1, 1, curYear, 1).Day){
				//third monday of january
				return "Martin Luther King Jr. Day";
			}
			
			else if(curMon==2 && curDay==numDayOfMonth(2, 3, curYear, 1).Day){
				//third monday of february
				return "Presidents' Day";
			}
			else if(curMon==5 && curDay==numDayOfMonth(5, 5, curYear, 1).Day){
				//last monday of may
				return "Memorial Day";
			}
			else if(curMon==7 && curDay==4){
				return "Independence Day";
			}
			else if(curMon==9 && curDay==numDayOfMonth(9, 1, curYear, 1).Day){
				//first monday of september
				return "Labor Day";
			}
			else if(curMon==10 && curDay==numDayOfMonth(10, 2, curYear, 1).Day){
				//second monday of october
				return "Columbus Day";
			}
			else if(curMon==11 && curDay==11){
				return "Veterans Day";
			}
			else if(curMon==11 && curDay==numDayOfMonth(11, 4, curYear, 4).Day){
				//fourth thursday of november
				return "Thanksgiving Day";
			}
			else if(curMon==12 && curDay==25){
				return "Christmas Day";
			}
			else{
				return "";
			}
		}
		
		
		private DateTime numDayOfMonth(int m, int d, int y, int weekDay){
			//returns the x day of the month eg. second tuesday of the month
			DateTime dt = new DateTime(0);
			try{
				dt = new DateTime(y, m, 1);
			}
			catch(ArgumentOutOfRangeException){
				DialogResult = MessageBox.Show("Invalid date was entered", "Invalid date error", MessageBoxButtons.OK);
			}
			int curXDay = 1; //used to compare to the x number of day we want to reach
			int valOfDay = (int)dt.DayOfWeek;
			while(valOfDay != weekDay){
				//move to the first occurrence of the day in the month
				dt = dt.AddDays(1);
				valOfDay = (valOfDay+1)%7;
			}
			//after setting the day to the first occurrence of the desired day check how many times it occurs in the month for case of last
			//since it could be 5 times in special cases
			if(d==5 && (dt.Day==1 || dt.Day==2 || dt.Day==3) && (m==1 || m==3 || m==5 || m==7 ||
		   	m==8 || m==10 || m==12)){
				d=5;
			}
			else if(d==5 && (dt.Day==1 || dt.Day==2) && (m==4 || m==6 || m==9 || m==11)){
				d=5;
			}
			else if(d==5 && (dt.Day==1) && m==2 && y%4==0){
				d=5;
			}
			else if(d==5){
				d=4;
			}
			while(curXDay != d){
				curXDay+=1;
				dt = dt.AddDays(7);
			}
			return dt;
		}
		
		private DateTime calculateDays(DateTime b, int d, int w, int m, int y){
			
			days.Text = d.ToString();
			weeks.Text = w.ToString();
			months.Text = m.ToString();
			years.Text = y.ToString();
			DialogResult mes;
			string messageCap = "Error invalid year result";
			MessageBoxButtons mesButton = MessageBoxButtons.OK;
			
			try{
				b = b.AddDays(d);
			}
			catch(ArgumentOutOfRangeException){
				mes = MessageBox.Show("A number of days were added resulting in an invalid year", messageCap, mesButton);
				return new DateTime(0);
			}
			try{
				b = b.AddDays(w*7);
			}
			catch(ArgumentOutOfRangeException){
				mes = MessageBox.Show("A number of weeks were added resulting in an invalid year", messageCap, mesButton);
				return new DateTime(0);
			}
			try{
				b = b.AddMonths(m);
			}
			catch(ArgumentOutOfRangeException){
				mes = MessageBox.Show("A number of months were added resulting in an invalid year", messageCap, mesButton);
				return new DateTime(0);
			}
			try{
				b = b.AddYears(y);
			}
			catch(ArgumentOutOfRangeException){
				mes = MessageBox.Show("A number of years were added resulting in an invalid year", messageCap, mesButton);
				return new DateTime(0);
			}
			return b;
		}
		
		
		private int businessDaysCalc(DateTime b, int d, int w, int m, int y){
			int businessDays = 0;
			
			//add all the business days from the days category
			for(int i=0; i<d; i++){
				try{
					b = b.AddDays(1);
					if(isWeekend(b)==""&&isHoliday(b)==""){
						businessDays++;
					}
				}
				catch(ArgumentOutOfRangeException){
					
				}
			}
			
			//add all the business days from the weeks category
			for(int i=0; i<w*7; i++){
				try{
					b = b.AddDays(1);
					if(isWeekend(b)==""&&isHoliday(b)==""){
						businessDays++;
					}
				}
				catch(ArgumentOutOfRangeException){
					
				}
			}
			
			//add all business days from the months category
			DateTime dtm = b.AddMonths(m);
			while(dtm != b){
				try{
					b = b.AddDays(1);
					if(isWeekend(b)==""&&isHoliday(b)==""){
						businessDays++;
					}
				}
				catch(ArgumentOutOfRangeException){
					
				}
			}
			
			//add all the business days from the years category
			DateTime dty = b.AddYears(y);
			while(dty != b){
				try{
					b = b.AddDays(1);
					if(isWeekend(b)==""&&isHoliday(b)==""){
						businessDays++;
					}
				}
				catch(ArgumentOutOfRangeException){
					
				}
			}
			return businessDays;
		}
		
		
	}
}
