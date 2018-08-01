using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using kin_kinit_mocker.Model.Earn;
using Newtonsoft.Json;

namespace kin_kinit_mocker.Repository
{
    public class TasksRepository
    {
        private const string QUESTIONNAIRE_ANSWERS_STORAGE = "kin.app.task.chosen.answers";
        private const string TASK_STORAGE = "kin.app.task";
        private const string TASK_KEY = "task";
        private const string TASK_STATE_KEY = "task_state";
        private readonly IDataStore _chosenAnswersCache;
        private readonly IDataStore _taskCache;
        private readonly List<ChosenAnswers> _chosenAnswers;
        private readonly string _taskStorageName;
        internal bool IsTaskStarted;

        public TasksRepository(IDataStoreProvider dataStoreProvider, string defaultTask = null)
        {
            _chosenAnswers = new List<ChosenAnswers>();
            _taskCache = dataStoreProvider.DataStore(TASK_STORAGE);
            EarnTask = GetCachedTask(defaultTask);
            _taskStorageName = QUESTIONNAIRE_ANSWERS_STORAGE + EarnTask?.Id;
            _chosenAnswersCache = dataStoreProvider.DataStore(_taskStorageName);
        }

        internal EarnTask EarnTask { get; private set; }

        public int TaskState
        {
            get => _taskCache.GetValue(TASK_STATE_KEY, (int) Model.TaskState.IDLE);
            set => _taskCache.PutValue(TASK_STATE_KEY, value);
        }

        internal int GetNumOfAnsweredQuestions => GetChosenAnswers().Count;
        internal bool IsTaskComplete => EarnTask?.Questions.Count == GetNumOfAnsweredQuestions;

        internal bool IsTaskAvaliable
        {
            get
            {
                long currDate = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long taskDate = EarnTask?.StartDateInMillis() ?? currDate;

                return currDate >= taskDate;
            }
        }

        internal void ResetTaskState()
        {
            TaskState = (int) Model.TaskState.IDLE;
        }

        private EarnTask GetCachedTask(string defaultTask)
        {
            string cachedTask = _taskCache.GetValue(TASK_KEY, defaultTask);

            if (cachedTask == null)
            {
                return new EarnTask();
            }

            return JsonConvert.DeserializeObject<EarnTask>(cachedTask);
        }

        internal void SetChosenAnswers(string questionId, List<string> answerIds)
        {
            _chosenAnswers.Add(new ChosenAnswers(questionId, answerIds));
            _chosenAnswersCache.PutValue(questionId, answerIds);
            IsTaskStarted = true;
        }

        internal List<string> GetChosenAnswersByQuestionId(string questionId)
        {
            return _chosenAnswersCache.GetValue<List<string>>(questionId);
        }

        internal List<ChosenAnswers> GetChosenAnswers()
        {
            if (_chosenAnswers.Count == 0)
            {
                ImmutableDictionary<string, object> answerMap = _chosenAnswersCache.GetAll();

                foreach (KeyValuePair<string, object> answers in answerMap)
                {
                    if (answers.Value == null)
                    {
                        continue;
                    }

                    if (answers.Value is string strValue)
                    {
                        _chosenAnswers.Add(new ChosenAnswers(answers.Key, new List<string> {strValue}));
                    }
                    else if (answers.Value is List<string> list)
                    {
                        _chosenAnswers.Add(new ChosenAnswers(answers.Key, list));
                    }
                }
            }

            return _chosenAnswers;
        }

        internal void ClearChosenAnswers()
        {
            _chosenAnswers.Clear();
            _chosenAnswersCache.ClearAll();
            IsTaskStarted = false;
        }

        internal void ReplaceTask(EarnTask earnTask = null)
        {
            EarnTask = earnTask;

            if (earnTask != null)
            {
                _taskCache.PutValue(TASK_KEY, JsonConvert.SerializeObject(earnTask));

                foreach (Question question in earnTask.Questions)
                {
                    if (question.HasImages())
                    {
                        //Implement downloading images later
                    }
                }
            }
            else
            {
                _taskCache.Clear(TASK_KEY);
            }

            ClearChosenAnswers();
        }
    }
}