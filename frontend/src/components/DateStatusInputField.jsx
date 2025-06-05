import React, { useState, useRef, useEffect } from "react";
import {
  format,
  parse,
  addMonths,
  subMonths,
  startOfMonth,
  endOfMonth,
  startOfWeek,
  endOfWeek,
  addDays,
  isSameMonth,
  isSameDay,
  isBefore,
  startOfDay,
  addYears,
  subYears,
  setMonth,
} from "date-fns";
import StatusInputField from "./StatusInputField";

export default function DateStatusInputField({ value, onChange }) {
  /* ─────────────────────────────  state  ───────────────────────────── */
  const today = startOfDay(new Date());
  const initialDate = value ? parse(value, "yyyy-MM-dd", new Date()) : today;

  const [selectedDate, setSelectedDate] = useState(initialDate);
  const [currentMonth, setCurrentMonth] = useState(startOfMonth(initialDate));
  const [showCalendar, setShowCalendar] = useState(false);
  const [viewMode, setViewMode] = useState('day'); // New state for view mode: 'day' or 'month'
  const wrapperRef = useRef(null);

  /* ─────────────────────  keep prop ↔ state in sync  ────────────────── */
  useEffect(() => {
    const newSelectedDate = value ? parse(value, "yyyy-MM-dd", new Date()) : today;
    setSelectedDate(newSelectedDate);
    setCurrentMonth(startOfMonth(newSelectedDate));
    setViewMode('day'); // Reset view mode when value prop changes
  }, [value]);

  useEffect(() => {
    if (!value && onChange) {
      onChange(format(today, "yyyy-MM-dd"));
    }
  }, []);

  /* ────────────────  close calendar on outside click  ──────────────── */
  useEffect(() => {
    function handleOutside(e) {
      if (wrapperRef.current && !wrapperRef.current.contains(e.target)) {
        setShowCalendar(false);
        setViewMode('day'); // Reset view mode when closing calendar
      }
    }
    document.addEventListener("mousedown", handleOutside);
    return () => document.removeEventListener("mousedown", handleOutside);
  }, []);

  /* ─────────────────────────  helpers  ─────────────────────────────── */
  const formattedDate = format(selectedDate, "dd/MM/yyyy");

  const navigateMonth = (direction) => {
    if (viewMode === 'day') {
      setCurrentMonth(direction === 'next' ? addMonths(currentMonth, 1) : subMonths(currentMonth, 1));
    } else if (viewMode === 'month') {
      setCurrentMonth(direction === 'next' ? addYears(currentMonth, 1) : subYears(currentMonth, 1));
    }
  };

  function onDateClick(day) {
    if (!isBefore(day, today)) {
      setSelectedDate(day);
      setShowCalendar(false);
      onChange?.(format(day, "yyyy-MM-dd"));
      setViewMode('day'); // Ensure view mode is day when a date is selected
    }
  }

  /* ────────────────  calendar grid builders  ──────────────── */
  const renderDayNames = () => (
    <div className="grid grid-cols-7 text-center text-gray-600 p-2">
      {["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"].map((d) => (
        <div key={d} className="py-1 font-medium">{d}</div>
      ))}
    </div>
  );

  const renderCalendarCells = () => {
    const monthStart = startOfMonth(currentMonth);
    const monthEnd = endOfMonth(monthStart);
    const startDate = startOfWeek(monthStart);
    const endDate = endOfWeek(monthEnd);

    const rows = [];
    let day = startDate;
    while (day <= endDate) {
      const week = [];
      for (let i = 0; i < 7; i++) {
        const clone = day;
        const isPastDay = isBefore(clone, today);
        const isCurrentMonthDay = isSameMonth(clone, monthStart);
        const isSelected = isSameDay(clone, selectedDate);

        week.push(
          <div
            key={clone.toISOString()}
            onClick={() => onDateClick(clone)}
            className={
              `text-center py-1 rounded-full ` +
              (isPastDay
                ? "text-gray-400 cursor-not-allowed opacity-60 "
                : isSameDay(clone, today)
                  ? "bg-blue-100 text-blue-700 font-semibold cursor-pointer "
                  : isSelected
                    ? "bg-blue-500 text-white cursor-pointer "
                    : isCurrentMonthDay
                      ? "text-gray-800 hover:bg-blue-100 cursor-pointer "
                      : "text-gray-400 cursor-pointer "
              )
            }
          >
            {format(clone, "d")}
          </div>
        );
        day = addDays(day, 1);
      }
      rows.push(<div key={day.toISOString()} className="grid grid-cols-7 gap-1">{week}</div>);
    }
    return <div className="space-y-1 m-2 p-2">{rows}</div>;
  };

  const renderMonthSelection = () => {
    const months = Array.from({ length: 12 }, (_, i) => i); // 0-11 for months

    return (
      <div className="grid grid-cols-3 gap-2 mt-4 text-center p-2">
        {months.map((monthIndex) => {
          const monthDate = setMonth(currentMonth, monthIndex); // Set month in the current year
          const isSelectedMonth = isSameMonth(monthDate, selectedDate);
          const isCurrentMonth = isSameMonth(monthDate, new Date()); // Check if it's the current month

          return (
            <div
              key={monthIndex}
              onClick={() => {
                setCurrentMonth(monthDate); // Update current month to the selected month's date
                setViewMode('day');         // Switch back to day view
              }}
              className={
                `py-1 rounded-lg cursor-pointer ` +
                (isSelectedMonth
                  ? "bg-blue-500 text-white "
                  : isCurrentMonth
                    ? "bg-blue-100 text-blue-700 font-semibold "
                    : "text-gray-800 hover:bg-blue-100 ")
              }
            >
              {format(monthDate, "MMM")} {/* Display short month name (e.g., Jan, Feb) */}
            </div>
          );
        })}
      </div>
    );
  };

  /* ───────────────────────────  ui  ────────────────────────────── */
  return (
    <div className="relative w-full" ref={wrapperRef}>
      {/* input box (keeps your StatusInputField styling) */}
      <div onClick={() => setShowCalendar((s) => !s)}>
        <StatusInputField
          label="Deadline"
          name="deadline"
          type="text"
          status="default"
          defaultValue={formattedDate}
          readOnly
          key={formattedDate}
        />
      </div>

      {/* calendar pop-up */}
      {showCalendar && (
        <div
          className="absolute top-full left-0 z-50 mt-2 bg-white border border-gray-200 rounded-lg shadow-lg
                     w-[calc(100%-1rem)] max-w-xs sm:max-w-sm md:w-64"
        >
          {/* header */}
          <div className="flex items-center justify-between px-3 py-2 bg-gray-100 rounded-t-lg">
            <button
              onClick={() => navigateMonth('prev')}
              className="text-gray-600 hover:text-gray-800 p-1 rounded-full"
            >
              &#8592;
            </button>
            <span
              className="text-gray-800 font-semibold cursor-pointer"
              onClick={() => setViewMode('month')}
            >
              {viewMode === 'day' ? format(currentMonth, "MMMM yyyy") : format(currentMonth, "yyyy")} {/* CORRECTED HERE! */}
            </span>
            <button
              onClick={() => navigateMonth('next')}
              className="text-gray-600 hover:text-gray-800 p-1 rounded-full"
            >
              &#8594;
            </button>
          </div>

          {/* Conditional rendering for day or month view */}
          {viewMode === 'day' && (
            <>
              {renderDayNames()}
              {renderCalendarCells()}
            </>
          )}
          {viewMode === 'month' && renderMonthSelection()}
        </div>
      )}
    </div>
  );
}